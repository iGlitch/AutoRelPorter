namespace AutomaticRelPorter
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOpenModded = new System.Windows.Forms.Button();
            this.modbox = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.messageBox = new System.Windows.Forms.TextBox();
            this.palButton = new System.Windows.Forms.RadioButton();
            this.jpnButton = new System.Windows.Forms.RadioButton();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // btnOpenModded
            // 
            this.btnOpenModded.Font = new System.Drawing.Font("Meiryo", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnOpenModded.Location = new System.Drawing.Point(26, 23);
            this.btnOpenModded.Name = "btnOpenModded";
            this.btnOpenModded.Size = new System.Drawing.Size(352, 72);
            this.btnOpenModded.TabIndex = 0;
            this.btnOpenModded.Text = "open rsbe module folder";
            this.btnOpenModded.UseVisualStyleBackColor = true;
            this.btnOpenModded.Click += new System.EventHandler(this.btnOpenModed_Click);
            // 
            // modbox
            // 
            this.modbox.Location = new System.Drawing.Point(63, 113);
            this.modbox.Name = "modbox";
            this.modbox.ReadOnly = true;
            this.modbox.Size = new System.Drawing.Size(279, 20);
            this.modbox.TabIndex = 1;
            this.modbox.TextChanged += new System.EventHandler(this.modbox_TextChanged);
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Meiryo", 15.75F, System.Drawing.FontStyle.Bold);
            this.btnStart.Location = new System.Drawing.Point(63, 149);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(279, 55);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "start porting";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // messageBox
            // 
            this.messageBox.Location = new System.Drawing.Point(26, 244);
            this.messageBox.Multiline = true;
            this.messageBox.Name = "messageBox";
            this.messageBox.ReadOnly = true;
            this.messageBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.messageBox.Size = new System.Drawing.Size(352, 212);
            this.messageBox.TabIndex = 3;
            // 
            // palButton
            // 
            this.palButton.AutoSize = true;
            this.palButton.Location = new System.Drawing.Point(297, 213);
            this.palButton.Name = "palButton";
            this.palButton.Size = new System.Drawing.Size(45, 17);
            this.palButton.TabIndex = 4;
            this.palButton.Text = "PAL";
            this.palButton.UseVisualStyleBackColor = true;
            // 
            // jpnButton
            // 
            this.jpnButton.AutoSize = true;
            this.jpnButton.Checked = true;
            this.jpnButton.Location = new System.Drawing.Point(63, 213);
            this.jpnButton.Name = "jpnButton";
            this.jpnButton.Size = new System.Drawing.Size(62, 17);
            this.jpnButton.TabIndex = 5;
            this.jpnButton.TabStop = true;
            this.jpnButton.Text = "NTSC-J";
            this.jpnButton.UseVisualStyleBackColor = true;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLabel1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabel1.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.linkLabel1.Location = new System.Drawing.Point(337, 459);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(43, 16);
            this.linkLabel1.TabIndex = 6;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "~Glitch";
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 487);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.jpnButton);
            this.Controls.Add(this.palButton);
            this.Controls.Add(this.messageBox);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.modbox);
            this.Controls.Add(this.btnOpenModded);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "RSBE Module Region Porter";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenModded;
        private System.Windows.Forms.TextBox modbox;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox messageBox;
        private System.Windows.Forms.RadioButton palButton;
        private System.Windows.Forms.RadioButton jpnButton;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

