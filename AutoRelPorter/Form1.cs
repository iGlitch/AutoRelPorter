using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace AutomaticRelPorter
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            if (!Directory.Exists("module"))
                Directory.CreateDirectory("module");
            if (!Directory.Exists("rsbe"))
                Directory.CreateDirectory("rsbe");
            if (!Directory.Exists("rsbj"))
                Directory.CreateDirectory("rsbj");
            if (!Directory.Exists("rsbp"))
                Directory.CreateDirectory("rsbp");
        }

        private void btnOpenModed_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog() { SelectedPath=@"C:\"};
            if (fbd.ShowDialog() != DialogResult.OK)
                return;
            modbox.Text = fbd.SelectedPath;
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(modbox.Text))
            {
                messageBox.AppendText("Select modded module folder");
                return;
            }
            var msg = new ObservableCollection<string>();
            msg.CollectionChanged += (obj, args) => 
            { 
                if(args.Action==System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                    Invoke(new Action(() => { messageBox.AppendText(args.NewItems[0] as string); }));
            };
            await Task.Run(new Action(()=> Program.Port(modbox.Text, jpnButton.Checked, msg)));
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void modbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.LinkVisited = true;
            System.Diagnostics.Process.Start("https://glitchery.jp");
        }
    }
}
