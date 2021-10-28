namespace Visu_datapoint_editor
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fájlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.megnyitásToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mentésToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bezárásToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.újToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 43);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1192, 540);
            this.dataGridView1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fájlToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1216, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fájlToolStripMenuItem
            // 
            this.fájlToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.újToolStripMenuItem,
            this.megnyitásToolStripMenuItem,
            this.mentésToolStripMenuItem,
            this.bezárásToolStripMenuItem});
            this.fájlToolStripMenuItem.Name = "fájlToolStripMenuItem";
            this.fájlToolStripMenuItem.Size = new System.Drawing.Size(36, 20);
            this.fájlToolStripMenuItem.Text = "Fájl";
            // 
            // megnyitásToolStripMenuItem
            // 
            this.megnyitásToolStripMenuItem.Name = "megnyitásToolStripMenuItem";
            this.megnyitásToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.megnyitásToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.megnyitásToolStripMenuItem.Text = "Megnyitás";
            this.megnyitásToolStripMenuItem.Click += new System.EventHandler(this.megnyitásToolStripMenuItem_Click);
            // 
            // mentésToolStripMenuItem
            // 
            this.mentésToolStripMenuItem.Name = "mentésToolStripMenuItem";
            this.mentésToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mentésToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.mentésToolStripMenuItem.Text = "Mentés";
            // 
            // bezárásToolStripMenuItem
            // 
            this.bezárásToolStripMenuItem.Name = "bezárásToolStripMenuItem";
            this.bezárásToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.bezárásToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.bezárásToolStripMenuItem.Text = "Bezárás";
            // 
            // újToolStripMenuItem
            // 
            this.újToolStripMenuItem.Name = "újToolStripMenuItem";
            this.újToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.újToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.újToolStripMenuItem.Text = "Új";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 595);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Datapoint editor";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fájlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem újToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem megnyitásToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mentésToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bezárásToolStripMenuItem;
    }
}

