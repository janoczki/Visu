using System.Windows.Forms;

namespace Visu_dataviewer.Forms
{
    partial class ScheduleReaderWriter
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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.comboBox5 = new System.Windows.Forms.ComboBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.objCovLabel = new System.Windows.Forms.Label();
            this.objInstLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.objTypeLabel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.devNetworkLabel = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.devInstLabel = new System.Windows.Forms.Label();
            this.devIPLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.checkBox1.Location = new System.Drawing.Point(19, 30);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(26, 31);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "Mo";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.checkBox2.Location = new System.Drawing.Point(51, 30);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(24, 31);
            this.checkBox2.TabIndex = 2;
            this.checkBox2.Text = "Tu";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.checkBox3.Location = new System.Drawing.Point(81, 30);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(28, 31);
            this.checkBox3.TabIndex = 3;
            this.checkBox3.Text = "We";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.checkBox4.Location = new System.Drawing.Point(115, 30);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(24, 31);
            this.checkBox4.TabIndex = 4;
            this.checkBox4.Text = "Th";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.checkBox5.Location = new System.Drawing.Point(145, 30);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(20, 31);
            this.checkBox5.TabIndex = 5;
            this.checkBox5.Text = "Fr";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.checkBox6.Location = new System.Drawing.Point(171, 30);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(24, 31);
            this.checkBox6.TabIndex = 6;
            this.checkBox6.Text = "Sa";
            this.checkBox6.UseVisualStyleBackColor = true;
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.checkBox7.Location = new System.Drawing.Point(201, 30);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(24, 31);
            this.checkBox7.TabIndex = 7;
            this.checkBox7.Text = "Su";
            this.checkBox7.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(19, 30);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(47, 21);
            this.comboBox1.TabIndex = 8;
            this.comboBox1.Text = "All";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 277);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(93, 277);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "Remove";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(252, 259);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Schedule command";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.comboBox4);
            this.groupBox2.Controls.Add(this.comboBox3);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.comboBox2);
            this.groupBox2.Controls.Add(this.comboBox1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(6, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(240, 76);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Time";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(175, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Hund";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBox4
            // 
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Location = new System.Drawing.Point(178, 30);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(47, 21);
            this.comboBox4.TabIndex = 18;
            this.comboBox4.Text = "All";
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(125, 30);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(47, 21);
            this.comboBox3.TabIndex = 18;
            this.comboBox3.Text = "All";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(125, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Sec";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(72, 30);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(47, 21);
            this.comboBox2.TabIndex = 20;
            this.comboBox2.Text = "All";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(72, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Min";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Hour";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.comboBox5);
            this.groupBox4.Controls.Add(this.numericUpDown1);
            this.groupBox4.Location = new System.Drawing.Point(6, 188);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(240, 65);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Action";
            // 
            // comboBox5
            // 
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.Location = new System.Drawing.Point(19, 30);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(96, 21);
            this.comboBox5.TabIndex = 16;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(129, 30);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            979930616,
            79,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(96, 20);
            this.numericUpDown1.TabIndex = 17;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Controls.Add(this.checkBox2);
            this.groupBox3.Controls.Add(this.checkBox3);
            this.groupBox3.Controls.Add(this.checkBox4);
            this.groupBox3.Controls.Add(this.checkBox5);
            this.groupBox3.Controls.Add(this.checkBox6);
            this.groupBox3.Controls.Add(this.checkBox7);
            this.groupBox3.Location = new System.Drawing.Point(6, 104);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(240, 74);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Day";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dataGridView2);
            this.groupBox5.Location = new System.Drawing.Point(12, 306);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(450, 271);
            this.groupBox5.TabIndex = 14;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Schedule list";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(6, 16);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(440, 249);
            this.dataGridView2.TabIndex = 1;
            this.dataGridView2.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView2_DataError);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(383, 583);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 15;
            this.button3.Text = "Send+close";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.groupBox7);
            this.groupBox6.Controls.Add(this.groupBox8);
            this.groupBox6.Location = new System.Drawing.Point(270, 12);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(192, 259);
            this.groupBox6.TabIndex = 20;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Bacnet";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.objCovLabel);
            this.groupBox7.Controls.Add(this.objInstLabel);
            this.groupBox7.Controls.Add(this.label5);
            this.groupBox7.Controls.Add(this.objTypeLabel);
            this.groupBox7.Controls.Add(this.label9);
            this.groupBox7.Controls.Add(this.label10);
            this.groupBox7.Location = new System.Drawing.Point(6, 140);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(182, 115);
            this.groupBox7.TabIndex = 19;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Object";
            // 
            // objCovLabel
            // 
            this.objCovLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.objCovLabel.Location = new System.Drawing.Point(67, 81);
            this.objCovLabel.Name = "objCovLabel";
            this.objCovLabel.Size = new System.Drawing.Size(100, 20);
            this.objCovLabel.TabIndex = 24;
            this.objCovLabel.Text = "Name";
            this.objCovLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // objInstLabel
            // 
            this.objInstLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.objInstLabel.Location = new System.Drawing.Point(67, 55);
            this.objInstLabel.Name = "objInstLabel";
            this.objInstLabel.Size = new System.Drawing.Size(100, 20);
            this.objInstLabel.TabIndex = 23;
            this.objInstLabel.Text = "Name";
            this.objInstLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "CoV";
            // 
            // objTypeLabel
            // 
            this.objTypeLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.objTypeLabel.Location = new System.Drawing.Point(67, 29);
            this.objTypeLabel.Name = "objTypeLabel";
            this.objTypeLabel.Size = new System.Drawing.Size(100, 20);
            this.objTypeLabel.TabIndex = 22;
            this.objTypeLabel.Text = "Name";
            this.objTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 60);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Instance";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 33);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "Type";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.devNetworkLabel);
            this.groupBox8.Controls.Add(this.label12);
            this.groupBox8.Controls.Add(this.devInstLabel);
            this.groupBox8.Controls.Add(this.devIPLabel);
            this.groupBox8.Controls.Add(this.label6);
            this.groupBox8.Controls.Add(this.label7);
            this.groupBox8.Location = new System.Drawing.Point(6, 19);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(182, 115);
            this.groupBox8.TabIndex = 18;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Device";
            // 
            // devNetworkLabel
            // 
            this.devNetworkLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.devNetworkLabel.Location = new System.Drawing.Point(67, 29);
            this.devNetworkLabel.Name = "devNetworkLabel";
            this.devNetworkLabel.Size = new System.Drawing.Size(100, 20);
            this.devNetworkLabel.TabIndex = 23;
            this.devNetworkLabel.Text = "Name";
            this.devNetworkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(16, 33);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(47, 13);
            this.label12.TabIndex = 22;
            this.label12.Text = "Network";
            // 
            // devInstLabel
            // 
            this.devInstLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.devInstLabel.Location = new System.Drawing.Point(67, 82);
            this.devInstLabel.Name = "devInstLabel";
            this.devInstLabel.Size = new System.Drawing.Size(100, 20);
            this.devInstLabel.TabIndex = 21;
            this.devInstLabel.Text = "Name";
            this.devInstLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // devIPLabel
            // 
            this.devIPLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.devIPLabel.Location = new System.Drawing.Point(67, 56);
            this.devIPLabel.Name = "devIPLabel";
            this.devIPLabel.Size = new System.Drawing.Size(100, 20);
            this.devIPLabel.TabIndex = 20;
            this.devIPLabel.Text = "Name";
            this.devIPLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Instance";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "IP addr";
            // 
            // ScheduleReaderWriter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 610);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "ScheduleReaderWriter";
            this.Text = "ScheduleReaderWriter";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ScheduleReaderWriter_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.CheckBox checkBox7;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox comboBox5;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.GroupBox groupBox5;
        public DataGridView dataGridView2;
        private Button button3;
        private GroupBox groupBox6;
        private GroupBox groupBox7;
        public Label objCovLabel;
        public Label objInstLabel;
        private Label label5;
        public Label objTypeLabel;
        private Label label9;
        private Label label10;
        private GroupBox groupBox8;
        public Label devNetworkLabel;
        private Label label12;
        public Label devInstLabel;
        public Label devIPLabel;
        private Label label6;
        private Label label7;
    }
}