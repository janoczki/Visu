using System.Windows.Forms;

namespace Visu_dataviewer.Forms
{
    partial class ReaderWriter
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
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.objCovLabel = new System.Windows.Forms.Label();
            this.objInstLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.objTypeLabel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.devInstLabel = new System.Windows.Forms.Label();
            this.devIPLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.recLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.typeLabel = new System.Windows.Forms.Label();
            this.descLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.resetButton = new System.Windows.Forms.Button();
            this.writeButton = new System.Windows.Forms.Button();
            this.valueToWriteTextbox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.readedValueLabel = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.groupBox3);
            this.groupBox5.Controls.Add(this.groupBox2);
            this.groupBox5.Location = new System.Drawing.Point(210, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(192, 234);
            this.groupBox5.TabIndex = 19;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Bacnet";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.objCovLabel);
            this.groupBox3.Controls.Add(this.objInstLabel);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.objTypeLabel);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Location = new System.Drawing.Point(6, 115);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(182, 115);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Object";
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "CoV";
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.devInstLabel);
            this.groupBox2.Controls.Add(this.devIPLabel);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(6, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(182, 90);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Device";
            // 
            // devInstLabel
            // 
            this.devInstLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.devInstLabel.Location = new System.Drawing.Point(67, 55);
            this.devInstLabel.Name = "devInstLabel";
            this.devInstLabel.Size = new System.Drawing.Size(100, 20);
            this.devInstLabel.TabIndex = 21;
            this.devInstLabel.Text = "Name";
            this.devInstLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // devIPLabel
            // 
            this.devIPLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.devIPLabel.Location = new System.Drawing.Point(67, 29);
            this.devIPLabel.Name = "devIPLabel";
            this.devIPLabel.Size = new System.Drawing.Size(100, 20);
            this.devIPLabel.TabIndex = 20;
            this.devIPLabel.Text = "Name";
            this.devIPLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Instance";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "IP addr";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.groupBox4);
            this.groupBox6.Controls.Add(this.groupBox1);
            this.groupBox6.Location = new System.Drawing.Point(12, 12);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(192, 205);
            this.groupBox6.TabIndex = 20;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "General";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.recLabel);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Location = new System.Drawing.Point(6, 140);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(182, 60);
            this.groupBox4.TabIndex = 19;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Record";
            // 
            // recLabel
            // 
            this.recLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.recLabel.Location = new System.Drawing.Point(67, 30);
            this.recLabel.Name = "recLabel";
            this.recLabel.Size = new System.Drawing.Size(100, 20);
            this.recLabel.TabIndex = 20;
            this.recLabel.Text = "Name";
            this.recLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Save";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.typeLabel);
            this.groupBox1.Controls.Add(this.descLabel);
            this.groupBox1.Controls.Add(this.nameLabel);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(182, 115);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Daatapoint";
            // 
            // typeLabel
            // 
            this.typeLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.typeLabel.Location = new System.Drawing.Point(67, 82);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(100, 20);
            this.typeLabel.TabIndex = 19;
            this.typeLabel.Text = "Name";
            this.typeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // descLabel
            // 
            this.descLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.descLabel.Location = new System.Drawing.Point(67, 55);
            this.descLabel.Name = "descLabel";
            this.descLabel.Size = new System.Drawing.Size(100, 20);
            this.descLabel.TabIndex = 18;
            this.descLabel.Text = "Name";
            this.descLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nameLabel
            // 
            this.nameLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.nameLabel.Location = new System.Drawing.Point(67, 29);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(100, 20);
            this.nameLabel.TabIndex = 17;
            this.nameLabel.Text = "Name";
            this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Desc";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name";
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(327, 433);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 22;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.groupBox8);
            this.groupBox7.Controls.Add(this.groupBox9);
            this.groupBox7.Location = new System.Drawing.Point(12, 252);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(390, 154);
            this.groupBox7.TabIndex = 23;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Read / write value";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.resetButton);
            this.groupBox8.Controls.Add(this.writeButton);
            this.groupBox8.Controls.Add(this.valueToWriteTextbox);
            this.groupBox8.Controls.Add(this.label11);
            this.groupBox8.Location = new System.Drawing.Point(6, 86);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(378, 60);
            this.groupBox8.TabIndex = 19;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Write";
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(261, 28);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 23;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // writeButton
            // 
            this.writeButton.Location = new System.Drawing.Point(180, 28);
            this.writeButton.Name = "writeButton";
            this.writeButton.Size = new System.Drawing.Size(75, 23);
            this.writeButton.TabIndex = 22;
            this.writeButton.Text = "Write";
            this.writeButton.UseVisualStyleBackColor = true;
            this.writeButton.Click += new System.EventHandler(this.writeButton_Click);
            // 
            // valueToWriteTextbox
            // 
            this.valueToWriteTextbox.Location = new System.Drawing.Point(67, 30);
            this.valueToWriteTextbox.MaxLength = 8;
            this.valueToWriteTextbox.Name = "valueToWriteTextbox";
            this.valueToWriteTextbox.Size = new System.Drawing.Size(100, 20);
            this.valueToWriteTextbox.TabIndex = 21;
            this.valueToWriteTextbox.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(16, 33);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(34, 13);
            this.label11.TabIndex = 20;
            this.label11.Text = "Value";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.readedValueLabel);
            this.groupBox9.Controls.Add(this.label17);
            this.groupBox9.Location = new System.Drawing.Point(6, 19);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(380, 61);
            this.groupBox9.TabIndex = 1;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Read";
            // 
            // readedValueLabel
            // 
            this.readedValueLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.readedValueLabel.Location = new System.Drawing.Point(67, 29);
            this.readedValueLabel.Name = "readedValueLabel";
            this.readedValueLabel.Size = new System.Drawing.Size(100, 20);
            this.readedValueLabel.TabIndex = 17;
            this.readedValueLabel.Text = "Name";
            this.readedValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(16, 33);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(34, 13);
            this.label17.TabIndex = 1;
            this.label17.Text = "Value";
            // 
            // ReaderWriter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 468);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Name = "ReaderWriter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ReaderWriter";
            this.TopMost = true;
            this.groupBox5.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button closeButton;
        public System.Windows.Forms.Label objCovLabel;
        public System.Windows.Forms.Label objInstLabel;
        public System.Windows.Forms.Label objTypeLabel;
        public System.Windows.Forms.Label devInstLabel;
        public System.Windows.Forms.Label devIPLabel;
        public System.Windows.Forms.Label recLabel;
        public System.Windows.Forms.Label typeLabel;
        public System.Windows.Forms.Label descLabel;
        public System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button writeButton;
        private System.Windows.Forms.TextBox valueToWriteTextbox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox9;
        public System.Windows.Forms.Label readedValueLabel;
        private System.Windows.Forms.Label label17;
        private Button resetButton;
    }
}