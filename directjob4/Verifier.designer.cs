namespace directjob4
{
    partial class Verifier
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
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.chercherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chercherDansLaPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.webBrowser2 = new System.Windows.Forms.WebBrowser();
            this.button_Reset = new System.Windows.Forms.Button();
            this.button_Show = new System.Windows.Forms.Button();
            this.button_Refresh = new System.Windows.Forms.Button();
            this.button_Forward = new System.Windows.Forms.Button();
            this.button_Back = new System.Windows.Forms.Button();
            this.button_Go = new System.Windows.Forms.Button();
            this.textBox_Url = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.AllowUserToOrderColumns = true;
            this.dataGridView3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView3.Location = new System.Drawing.Point(0, 0);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.ReadOnly = true;
            this.dataGridView3.Size = new System.Drawing.Size(650, 228);
            this.dataGridView3.TabIndex = 2;
            this.dataGridView3.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMousDouble);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Controls.Add(this.button_Reset);
            this.panel1.Controls.Add(this.button_Show);
            this.panel1.Controls.Add(this.button_Refresh);
            this.panel1.Controls.Add(this.button_Forward);
            this.panel1.Controls.Add(this.button_Back);
            this.panel1.Controls.Add(this.button_Go);
            this.panel1.Controls.Add(this.textBox_Url);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1241, 545);
            this.panel1.TabIndex = 3;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(3, 38);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.webBrowser1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1236, 501);
            this.splitContainer1.SplitterDistance = 582;
            this.splitContainer1.TabIndex = 12;
            // 
            // webBrowser1
            // 
            this.webBrowser1.ContextMenuStrip = this.contextMenuStrip1;
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(582, 501);
            this.webBrowser1.TabIndex = 3;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chercherToolStripMenuItem,
            this.chercherDansLaPageToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(192, 48);
            // 
            // chercherToolStripMenuItem
            // 
            this.chercherToolStripMenuItem.Enabled = false;
            this.chercherToolStripMenuItem.Name = "chercherToolStripMenuItem";
            this.chercherToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.chercherToolStripMenuItem.Text = "Chercher par lien";
            this.chercherToolStripMenuItem.Click += new System.EventHandler(this.chercherToolStripMenuItem_Click);
            // 
            // chercherDansLaPageToolStripMenuItem
            // 
            this.chercherDansLaPageToolStripMenuItem.Enabled = false;
            this.chercherDansLaPageToolStripMenuItem.Name = "chercherDansLaPageToolStripMenuItem";
            this.chercherDansLaPageToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.chercherDansLaPageToolStripMenuItem.Text = "Chercher dans la page";
            this.chercherDansLaPageToolStripMenuItem.Click += new System.EventHandler(this.chercherDansLaPageToolStripMenuItem_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dataGridView3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.webBrowser2);
            this.splitContainer2.Size = new System.Drawing.Size(650, 501);
            this.splitContainer2.SplitterDistance = 228;
            this.splitContainer2.TabIndex = 13;
            // 
            // webBrowser2
            // 
            this.webBrowser2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser2.Location = new System.Drawing.Point(0, 0);
            this.webBrowser2.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser2.Name = "webBrowser2";
            this.webBrowser2.ScriptErrorsSuppressed = true;
            this.webBrowser2.Size = new System.Drawing.Size(650, 269);
            this.webBrowser2.TabIndex = 10;
            // 
            // button_Reset
            // 
            this.button_Reset.Location = new System.Drawing.Point(744, 8);
            this.button_Reset.Name = "button_Reset";
            this.button_Reset.Size = new System.Drawing.Size(92, 23);
            this.button_Reset.TabIndex = 11;
            this.button_Reset.Text = "Reset Data";
            this.button_Reset.UseVisualStyleBackColor = true;
            this.button_Reset.Click += new System.EventHandler(this.button_Reset_Click);
            // 
            // button_Show
            // 
            this.button_Show.Location = new System.Drawing.Point(643, 9);
            this.button_Show.Name = "button_Show";
            this.button_Show.Size = new System.Drawing.Size(95, 23);
            this.button_Show.TabIndex = 9;
            this.button_Show.Text = "Show/NoShow";
            this.button_Show.UseVisualStyleBackColor = true;
            // 
            // button_Refresh
            // 
            this.button_Refresh.Location = new System.Drawing.Point(114, 9);
            this.button_Refresh.Name = "button_Refresh";
            this.button_Refresh.Size = new System.Drawing.Size(57, 23);
            this.button_Refresh.TabIndex = 8;
            this.button_Refresh.Text = "Refresh";
            this.button_Refresh.UseVisualStyleBackColor = true;
            this.button_Refresh.Click += new System.EventHandler(this.button_Refresh_Click);
            // 
            // button_Forward
            // 
            this.button_Forward.Location = new System.Drawing.Point(55, 9);
            this.button_Forward.Name = "button_Forward";
            this.button_Forward.Size = new System.Drawing.Size(53, 23);
            this.button_Forward.TabIndex = 7;
            this.button_Forward.Text = "Forward";
            this.button_Forward.UseVisualStyleBackColor = true;
            this.button_Forward.Click += new System.EventHandler(this.button_Forward_Click);
            // 
            // button_Back
            // 
            this.button_Back.Location = new System.Drawing.Point(3, 9);
            this.button_Back.Name = "button_Back";
            this.button_Back.Size = new System.Drawing.Size(46, 23);
            this.button_Back.TabIndex = 6;
            this.button_Back.Text = "Back";
            this.button_Back.UseVisualStyleBackColor = true;
            this.button_Back.Click += new System.EventHandler(this.button_Back_Click);
            // 
            // button_Go
            // 
            this.button_Go.Location = new System.Drawing.Point(590, 9);
            this.button_Go.Name = "button_Go";
            this.button_Go.Size = new System.Drawing.Size(47, 23);
            this.button_Go.TabIndex = 5;
            this.button_Go.Text = "Go";
            this.button_Go.UseVisualStyleBackColor = true;
            this.button_Go.Click += new System.EventHandler(this.button_Go_Click);
            // 
            // textBox_Url
            // 
            this.textBox_Url.Location = new System.Drawing.Point(177, 11);
            this.textBox_Url.Name = "textBox_Url";
            this.textBox_Url.Size = new System.Drawing.Size(407, 20);
            this.textBox_Url.TabIndex = 4;
            // 
            // Verifier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1241, 545);
            this.Controls.Add(this.panel1);
            this.Name = "Verifier";
            this.Text = "Verifier";
            this.Load += new System.EventHandler(this.Verifier_Load);
            this.SizeChanged += new System.EventHandler(this.Verifier_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_Refresh;
        private System.Windows.Forms.Button button_Forward;
        private System.Windows.Forms.Button button_Back;
        private System.Windows.Forms.Button button_Go;
        private System.Windows.Forms.TextBox textBox_Url;
        private System.Windows.Forms.WebBrowser webBrowser2;
        private System.Windows.Forms.Button button_Show;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem chercherToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chercherDansLaPageToolStripMenuItem;
        private System.Windows.Forms.Button button_Reset;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;

    }
}