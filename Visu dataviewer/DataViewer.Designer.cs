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
            this.pollingBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.fájlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.megnyitásToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bezárásToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UItimer = new System.Windows.Forms.Timer(this.components);
            this.covSubscriptionTimer = new System.Windows.Forms.Timer(this.components);
            this.listView1 = new Visu_dataviewer.ListViewNF();
            this.pollingButton = new System.Windows.Forms.Button();
            this.pollingIntervalTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // onlineButton
            // 
            this.onlineButton.Location = new System.Drawing.Point(12, 547);
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
            // pollingBackgroundWorker
            // 
            this.pollingBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.pollingBackgroundworker_DoWork);
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fájlToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(1152, 24);
            this.menu.TabIndex = 2;
            this.menu.Text = "menuStrip1";
            // 
            // fájlToolStripMenuItem
            // 
            this.fájlToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.megnyitásToolStripMenuItem,
            this.bezárásToolStripMenuItem});
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
            // UItimer
            // 
            this.UItimer.Interval = 50;
            this.UItimer.Tick += new System.EventHandler(this.UITimer_Tick);
            // 
            // covSubscriptionTimer
            // 
            this.covSubscriptionTimer.Tick += new System.EventHandler(this.covSubscriptionTimer_Tick);
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(12, 27);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1128, 514);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
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
            // DataViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1152, 611);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pollingIntervalTextbox);
            this.Controls.Add(this.pollingButton);
            this.Controls.Add(this.onlineButton);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.menu);
            this.MainMenuStrip = this.menu;
            this.Name = "DataViewer";
            this.Text = "DataViewer";
            this.Load += new System.EventHandler(this.DataViewer_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        //private System.Windows.Forms.ListView listView1;
        private ListViewNF listView1;
        private System.Windows.Forms.Button onlineButton;
        private System.Windows.Forms.Timer pollingTimer;
        private System.ComponentModel.BackgroundWorker pollingBackgroundWorker;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem fájlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem megnyitásToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bezárásToolStripMenuItem;
        private System.Windows.Forms.Timer UItimer;
        private System.Windows.Forms.Timer covSubscriptionTimer;
        private System.Windows.Forms.Button pollingButton;
        private System.Windows.Forms.TextBox pollingIntervalTextbox;
        private System.Windows.Forms.Label label1;
    }
}

