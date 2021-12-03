namespace Visu_dataviewer
{
    partial class DataViewer
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
            this.onlineButton = new System.Windows.Forms.Button();
            this.pollingTimer = new System.Windows.Forms.Timer(this.components);
            this.menu = new System.Windows.Forms.MenuStrip();
            this.fájlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.megnyitásToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bezárásToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sqlConnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UItimer = new System.Windows.Forms.Timer(this.components);
            this.covSubscriptionTimer = new System.Windows.Forms.Timer(this.components);
            this.pollingButton = new System.Windows.Forms.Button();
            this.pollingIntervalTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.subscribeButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.listView1 = new Visu_dataviewer.ListViewNF();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // onlineButton
            // 
            this.onlineButton.Location = new System.Drawing.Point(12, 518);
            this.onlineButton.Name = "onlineButton";
            this.onlineButton.Size = new System.Drawing.Size(75, 23);
            this.onlineButton.TabIndex = 1;
            this.onlineButton.Text = "Online";
            this.onlineButton.UseVisualStyleBackColor = true;
            this.onlineButton.Click += new System.EventHandler(this.onlineButton_Click);
            // 
            // pollingTimer
            // 
            this.pollingTimer.Tick += new System.EventHandler(this.pollingTimer_Tick);
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fájlToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(1912, 24);
            this.menu.TabIndex = 2;
            this.menu.Text = "menuStrip1";
            // 
            // fájlToolStripMenuItem
            // 
            this.fájlToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.megnyitásToolStripMenuItem,
            this.bezárásToolStripMenuItem,
            this.sqlConnectToolStripMenuItem});
            this.fájlToolStripMenuItem.Name = "fájlToolStripMenuItem";
            this.fájlToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.fájlToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fájlToolStripMenuItem.Text = "File";
            // 
            // megnyitásToolStripMenuItem
            // 
            this.megnyitásToolStripMenuItem.Name = "megnyitásToolStripMenuItem";
            this.megnyitásToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.megnyitásToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.megnyitásToolStripMenuItem.Text = "Open";
            this.megnyitásToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // bezárásToolStripMenuItem
            // 
            this.bezárásToolStripMenuItem.Name = "bezárásToolStripMenuItem";
            this.bezárásToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.bezárásToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.bezárásToolStripMenuItem.Text = "Quit";
            this.bezárásToolStripMenuItem.Click += new System.EventHandler(this.QuitToolStripMenuItem_Click);
            // 
            // sqlConnectToolStripMenuItem
            // 
            this.sqlConnectToolStripMenuItem.Name = "sqlConnectToolStripMenuItem";
            this.sqlConnectToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.sqlConnectToolStripMenuItem.Text = "Sql connect";
            this.sqlConnectToolStripMenuItem.Click += new System.EventHandler(this.sqlConnectToolStripMenuItem_Click);
            // 
            // UItimer
            // 
            this.UItimer.Interval = 200;
            this.UItimer.Tick += new System.EventHandler(this.UITimer_Tick);
            // 
            // covSubscriptionTimer
            // 
            this.covSubscriptionTimer.Tick += new System.EventHandler(this.covSubscriptionTimer_Tick);
            // 
            // pollingButton
            // 
            this.pollingButton.Enabled = false;
            this.pollingButton.Location = new System.Drawing.Point(12, 576);
            this.pollingButton.Name = "pollingButton";
            this.pollingButton.Size = new System.Drawing.Size(75, 23);
            this.pollingButton.TabIndex = 3;
            this.pollingButton.Text = "Start polling";
            this.pollingButton.UseVisualStyleBackColor = true;
            this.pollingButton.Click += new System.EventHandler(this.pollingButton_Click);
            // 
            // pollingIntervalTextbox
            // 
            this.pollingIntervalTextbox.Location = new System.Drawing.Point(93, 578);
            this.pollingIntervalTextbox.Name = "pollingIntervalTextbox";
            this.pollingIntervalTextbox.Size = new System.Drawing.Size(49, 20);
            this.pollingIntervalTextbox.TabIndex = 4;
            this.pollingIntervalTextbox.Text = "5";
            this.pollingIntervalTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(148, 581);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "second(s)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // subscribeButton
            // 
            this.subscribeButton.Enabled = false;
            this.subscribeButton.Location = new System.Drawing.Point(12, 547);
            this.subscribeButton.Name = "subscribeButton";
            this.subscribeButton.Size = new System.Drawing.Size(75, 23);
            this.subscribeButton.TabIndex = 6;
            this.subscribeButton.Text = "Subscribe";
            this.subscribeButton.UseVisualStyleBackColor = true;
            this.subscribeButton.Click += new System.EventHandler(this.subscribeButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(344, 518);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "timeprogram read";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(446, 518);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(102, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "timeprogram write";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(673, 553);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(135, 23);
            this.button3.TabIndex = 9;
            this.button3.Text = "ScheduleReadWrite";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // listView1
            // 
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(12, 27);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1888, 475);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            // 
            // DataViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1912, 630);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.subscribeButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pollingIntervalTextbox);
            this.Controls.Add(this.pollingButton);
            this.Controls.Add(this.onlineButton);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.menu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.menu;
            this.MaximizeBox = false;
            this.Name = "DataViewer";
            this.Text = "DataViewer";
            this.Load += new System.EventHandler(this.DataViewer_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button onlineButton;
        private System.Windows.Forms.Timer pollingTimer;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem fájlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem megnyitásToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bezárásToolStripMenuItem;
        public System.Windows.Forms.Timer UItimer;
        private System.Windows.Forms.Timer covSubscriptionTimer;
        private System.Windows.Forms.Button pollingButton;
        private System.Windows.Forms.TextBox pollingIntervalTextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button subscribeButton;
        private System.Windows.Forms.ToolStripMenuItem sqlConnectToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        public ListViewNF listView1;
    }
}

