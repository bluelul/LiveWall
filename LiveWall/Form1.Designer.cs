namespace LiveWall
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnChooseWall = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblNamedetail = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cbxStartup = new System.Windows.Forms.CheckBox();
            this.lblfilename = new System.Windows.Forms.Label();
            this.lblsyntaxTitle = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnChooseWall
            // 
            this.btnChooseWall.Location = new System.Drawing.Point(10, 11);
            this.btnChooseWall.Name = "btnChooseWall";
            this.btnChooseWall.Size = new System.Drawing.Size(122, 103);
            this.btnChooseWall.TabIndex = 0;
            this.btnChooseWall.Text = "Choose\r\nWallpapers";
            this.btnChooseWall.UseVisualStyleBackColor = true;
            this.btnChooseWall.Click += new System.EventHandler(this.btnChooseWall_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "LiveWall";
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblNamedetail
            // 
            this.lblNamedetail.AutoSize = true;
            this.lblNamedetail.Location = new System.Drawing.Point(141, 49);
            this.lblNamedetail.Name = "lblNamedetail";
            this.lblNamedetail.Size = new System.Drawing.Size(103, 39);
            this.lblNamedetail.TabIndex = 6;
            this.lblNamedetail.Text = "hh: hour [0:23] \r\nD: day of week [2:8]\r\next: png, jpg, bmp,...";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(259, 146);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.cbxStartup);
            this.tabPage1.Controls.Add(this.lblfilename);
            this.tabPage1.Controls.Add(this.lblsyntaxTitle);
            this.tabPage1.Controls.Add(this.btnChooseWall);
            this.tabPage1.Controls.Add(this.lblNamedetail);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(251, 120);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            // 
            // cbxStartup
            // 
            this.cbxStartup.AutoSize = true;
            this.cbxStartup.Checked = true;
            this.cbxStartup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxStartup.Location = new System.Drawing.Point(141, 97);
            this.cbxStartup.Name = "cbxStartup";
            this.cbxStartup.Size = new System.Drawing.Size(96, 17);
            this.cbxStartup.TabIndex = 9;
            this.cbxStartup.Text = "Run at start up";
            this.cbxStartup.UseVisualStyleBackColor = true;
            this.cbxStartup.CheckedChanged += new System.EventHandler(this.cbxStartup_CheckedChanged);
            // 
            // lblfilename
            // 
            this.lblfilename.AutoSize = true;
            this.lblfilename.Location = new System.Drawing.Point(141, 30);
            this.lblfilename.Name = "lblfilename";
            this.lblfilename.Size = new System.Drawing.Size(88, 13);
            this.lblfilename.TabIndex = 8;
            this.lblfilename.Text = "hhD.ext or hh.ext";
            // 
            // lblsyntaxTitle
            // 
            this.lblsyntaxTitle.AutoSize = true;
            this.lblsyntaxTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblsyntaxTitle.Location = new System.Drawing.Point(139, 13);
            this.lblsyntaxTitle.Name = "lblsyntaxTitle";
            this.lblsyntaxTitle.Size = new System.Drawing.Size(87, 13);
            this.lblsyntaxTitle.TabIndex = 7;
            this.lblsyntaxTitle.Text = "Image Syntax:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(251, 120);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "About";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(240, 91);
            this.label2.TabIndex = 8;
            this.label2.Text = "Change wallpaper following time and day of week\r\n\r\nVersion: 1.0\r\n\r\nAuthor: lttung" +
                "1197\r\n\r\nRelease date: 01/2020\r\n";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(105, 26);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(256, 146);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LiveWall";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnChooseWall;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblNamedetail;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lblfilename;
        private System.Windows.Forms.Label lblsyntaxTitle;
        private System.Windows.Forms.CheckBox cbxStartup;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
    }
}

