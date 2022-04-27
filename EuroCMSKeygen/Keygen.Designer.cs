namespace EuroCMSKeygen
{
    partial class Keygen
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
            this.txtDomains = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.btnEncode = new System.Windows.Forms.Button();
            this.btnDecode = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.machineKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtDomains
            // 
            this.txtDomains.Location = new System.Drawing.Point(13, 71);
            this.txtDomains.Multiline = true;
            this.txtDomains.Name = "txtDomains";
            this.txtDomains.Size = new System.Drawing.Size(515, 94);
            this.txtDomains.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Domains (Comma Seperated)";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 181);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Generated Key";
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(15, 203);
            this.txtKey.Multiline = true;
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(515, 94);
            this.txtKey.TabIndex = 2;
            // 
            // btnEncode
            // 
            this.btnEncode.Location = new System.Drawing.Point(13, 317);
            this.btnEncode.Name = "btnEncode";
            this.btnEncode.Size = new System.Drawing.Size(80, 23);
            this.btnEncode.TabIndex = 4;
            this.btnEncode.Text = "Encode";
            this.btnEncode.UseVisualStyleBackColor = true;
            this.btnEncode.Click += new System.EventHandler(this.btnEncode_Click);
            // 
            // btnDecode
            // 
            this.btnDecode.Location = new System.Drawing.Point(109, 317);
            this.btnDecode.Name = "btnDecode";
            this.btnDecode.Size = new System.Drawing.Size(80, 23);
            this.btnDecode.TabIndex = 5;
            this.btnDecode.Text = "Decode";
            this.btnDecode.UseVisualStyleBackColor = true;
            this.btnDecode.Click += new System.EventHandler(this.btnDecode_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(546, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.machineKeyToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // machineKeyToolStripMenuItem
            // 
            this.machineKeyToolStripMenuItem.Name = "machineKeyToolStripMenuItem";
            this.machineKeyToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.machineKeyToolStripMenuItem.Text = "Machine Key";
            this.machineKeyToolStripMenuItem.Click += new System.EventHandler(this.machineKeyToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(351, 317);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "VB Encode";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnVBEncode_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(449, 317);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(82, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "VB Decode";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnVBDecode_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(219, 317);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(100, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "VB To C#";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.btnVBToCShapDecode_Click);
            // 
            // Keygen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 355);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnDecode);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnEncode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDomains);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Keygen";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDomains;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Button btnEncode;
        private System.Windows.Forms.Button btnDecode;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem machineKeyToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}

