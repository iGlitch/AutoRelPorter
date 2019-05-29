using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AutomaticRelPorter
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var args = Environment.GetCommandLineArgs();

            Application.Run(new MainForm());
        }

        private static string GuessOriginal(string mod, string[] basefiles)
        {
            var size = new FileInfo(mod).Length;
            foreach (var f in basefiles)
                if (size == new FileInfo(f).Length)
                    return f;
            return null;
        }

        private static void ReplacePatch(List<byte> patch, byte[] subpatch, byte[] replpatch)
        {
            if (subpatch.Length != replpatch.Length)
                throw new ArgumentException("Invalid length of subpatch or replpatch");

            for (var i = 0; i < patch.Count - subpatch.Length; i++)
                for (var j = 0; j < subpatch.Length; j++)
                    if (patch[i + j] != subpatch[j])
                        break;
                    else if (j == subpatch.Length - 1)
                    {
                        for (var k = 0; k < replpatch.Length; k++)
                            patch[i + k] = replpatch[k];
                        break;
                    }
        }

        public static void Port(string modDir, bool toJpn, ObservableCollection<string> msgBuf)
        {
            var files = Directory.GetFiles(modDir);
            var basefiles = Directory.GetFiles("rsbe");
            //var errors = new string[files.Length];
            //var eind=0;
#if !DEBUG
            Parallel.ForEach<string>(files, new Action<string>(f =>
#else
            foreach(var f in files)
#endif
            {
                string b = GuessOriginal(f, basefiles);
                string jpn = null;
                var bin = new byte[0x1000 * 0x500];
                var binb = new byte[0x1000 * 0x500];
                var binJpn = new byte[0x1000 * 0x500];
                //Tuple < "sample" "patch" >
                var samplePatchPair = new List<Tuple<List<byte>, List<byte>>>();//上から順にパッチが入っていることを保証しなければならない

                if (b == null)
                {
                    lock (msgBuf)
                    {
                        msgBuf.Add(String.Format("RSBEファイルが存在しません。({0})\n", Path.GetFileName(f)));
                    }
                    return;
                }
                if (toJpn && !File.Exists(jpn = "RSBJ//" + Path.GetFileName(b)))
                {
                    lock (msgBuf)
                    {
                        msgBuf.Add(String.Format("RSBJファイルが存在しません。({0})\n", Path.GetFileName(f)));
                    }
                    return;
                }
                else if (!toJpn && !File.Exists(jpn = "RSBP//" + Path.GetFileName(b)))
                {
                    lock (msgBuf)
                    {
                        msgBuf.Add(String.Format("RSBPファイルが存在しません。 ({0})\n", Path.GetFileName(f)));
                    }
                    return;
                }
                using (var stream = new FileStream(f, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (var streamb = new FileStream(b, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (var streamJpn = new FileStream(jpn, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (var streamResult = new FileStream("module//" + Path.GetFileName(f), FileMode.Create))
                {
                    stream.Read(bin, 0, (int)stream.Length);
                    streamb.Read(binb, 0, (int)stream.Length);
                    streamJpn.Read(binJpn, 0, (int)streamJpn.Length);

                    var i = 0;

                    while (i < stream.Length)//差分探索
                    {
                        for (; i < stream.Length && bin[i] == binb[i]; i++) ;//seek to diff
                        var sample = new List<byte>();
                        var patch = new List<byte>();
                        for (; i < stream.Length && bin[i] != binb[i]; i++)
                        {
                            sample.Add(binb[i]);
                            patch.Add(bin[i]);
                        }
                        //更にサンプルは多めに取る
                        if (sample.Count < 0x10)
                            for (var ind = 0; ind < 0x10; ind++)
                                sample.Add(binb[i + ind]);

                        //Character ID
                        if (toJpn)
                            ReplacePatch(patch, new byte[] { 0x8E, 0xE0 }, new byte[] { 0x8A, 0x60 });
                        //LA expansion
                        if (toJpn)
                            ReplacePatch(patch, new byte[] { 0x61, 0x6B, 0x55, 0x40 }, new byte[] { 0x61, 0x6B, 0x50, 0xC0 });
                        if (sample.Count > 0)
                            samplePatchPair.Add(new Tuple<List<byte>, List<byte>>(sample, patch));
                    }

                    var j = 0;
                    for (int index = 0; index < samplePatchPair.Count; index++)
                    {
                        var t = samplePatchPair[index];
                        while (j < streamJpn.Length)
                        {
                            for (int k = j, l = 0; k < streamJpn.Length && l < t.Item1.Count && binJpn[k] == t.Item1[l]; k++, l++)
                                if (l == t.Item1.Count - 1)
                                {
                                    //発見
                                    for (l = 0; l < t.Item2.Count; j++, l++)
                                        binJpn[j] = t.Item2[l];
                                    goto LOOPEND;
                                }
                            j++;
                            /*throw new NotImplementedException();
                            while (j < streamJpn.Length && binJpn[j] != t.Item1[0]) j++;//該当箇所までシーク
                            for (var k = j; k < streamJpn.Length && binJpn[k] == t.Item1[k - j]; k++)//該当するかチェック
                            {
                                if (k - j == t.Item1.Count - 1)//該当
                                {
                                    for (k = j; k - j < t.Item2.Count; k++)
                                    {
                                        binJpn[k] = t.Item2[k - j];//patch
                                    }
                                    j += t.Item2.Count;//ポインタを進める
                                    goto LOOPEND;//次のパッチ箇所へ
                                }
                            }
                            j++;*/
                        }
                    LOOPEND: ;
                    }
                    streamResult.Write(binJpn, 0, (int)streamJpn.Length);
                    msgBuf.Add(String.Format("Finished {0}\n", Path.GetFileName(f)));
                }
            }
#if !DEBUG
));
#endif
            msgBuf.Add("Porting finished\n");
        }
    }
}
