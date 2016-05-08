namespace AddStrip
{
    partial class AddStripForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lstCalculations = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNextCalculation = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnInsertCalculation = new System.Windows.Forms.Button();
            this.btnDeleteCalculation = new System.Windows.Forms.Button();
            this.btnUpdateCalculation = new System.Windows.Forms.Button();
            this.txtSelectedCalculation = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(555, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "mnuFile";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.printToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.saveToolStripMenuItem.Text = "Save...";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(141, 6);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.printToolStripMenuItem.Text = "Print";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(141, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // lstCalculations
            // 
            this.lstCalculations.FormattingEnabled = true;
            this.lstCalculations.ItemHeight = 16;
            this.lstCalculations.Location = new System.Drawing.Point(36, 28);
            this.lstCalculations.Name = "lstCalculations";
            this.lstCalculations.Size = new System.Drawing.Size(181, 292);
            this.lstCalculations.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(266, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 38);
            this.label1.TabIndex = 2;
            this.label1.Text = "Enter your calculations in the text box below:";
            // 
            // txtNextCalculation
            // 
            this.txtNextCalculation.Location = new System.Drawing.Point(269, 74);
            this.txtNextCalculation.Name = "txtNextCalculation";
            this.txtNextCalculation.Size = new System.Drawing.Size(149, 22);
            this.txtNextCalculation.TabIndex = 3;
            this.txtNextCalculation.TextChanged += new System.EventHandler(this.txtCalculation_TextChanged);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(266, 180);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(190, 38);
            this.label2.TabIndex = 4;
            this.label2.Text = "To make changes select a line in the list first";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.btnInsertCalculation);
            this.panel1.Controls.Add(this.btnDeleteCalculation);
            this.panel1.Controls.Add(this.btnUpdateCalculation);
            this.panel1.Controls.Add(this.txtSelectedCalculation);
            this.panel1.Location = new System.Drawing.Point(269, 222);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(252, 98);
            this.panel1.TabIndex = 5;
            // 
            // btnInsertCalculation
            // 
            this.btnInsertCalculation.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInsertCalculation.Location = new System.Drawing.Point(165, 55);
            this.btnInsertCalculation.Name = "btnInsertCalculation";
            this.btnInsertCalculation.Size = new System.Drawing.Size(70, 30);
            this.btnInsertCalculation.TabIndex = 9;
            this.btnInsertCalculation.Text = "Insert";
            this.btnInsertCalculation.UseVisualStyleBackColor = true;
            this.btnInsertCalculation.Click += new System.EventHandler(this.btnInsertCalculation_Click);
            // 
            // btnDeleteCalculation
            // 
            this.btnDeleteCalculation.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteCalculation.Location = new System.Drawing.Point(89, 55);
            this.btnDeleteCalculation.Name = "btnDeleteCalculation";
            this.btnDeleteCalculation.Size = new System.Drawing.Size(70, 30);
            this.btnDeleteCalculation.TabIndex = 8;
            this.btnDeleteCalculation.Text = "Delete";
            this.btnDeleteCalculation.UseVisualStyleBackColor = true;
            this.btnDeleteCalculation.Click += new System.EventHandler(this.btnDeleteCalculation_Click);
            // 
            // btnUpdateCalculation
            // 
            this.btnUpdateCalculation.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateCalculation.Location = new System.Drawing.Point(13, 55);
            this.btnUpdateCalculation.Name = "btnUpdateCalculation";
            this.btnUpdateCalculation.Size = new System.Drawing.Size(70, 30);
            this.btnUpdateCalculation.TabIndex = 7;
            this.btnUpdateCalculation.Text = "Update";
            this.btnUpdateCalculation.UseVisualStyleBackColor = true;
            this.btnUpdateCalculation.Click += new System.EventHandler(this.btnUpdateCalculation_Click);
            // 
            // txtSelectedCalculation
            // 
            this.txtSelectedCalculation.Location = new System.Drawing.Point(49, 15);
            this.txtSelectedCalculation.Name = "txtSelectedCalculation";
            this.txtSelectedCalculation.Size = new System.Drawing.Size(149, 22);
            this.txtSelectedCalculation.TabIndex = 6;
            this.txtSelectedCalculation.TextChanged += new System.EventHandler(this.txtCalculation_TextChanged);
            // 
            // AddStripForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 355);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNextCalculation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstCalculations);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "AddStripForm";
            this.Text = "AddStripForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddStripForm_FormClosing);
            this.Load += new System.EventHandler(this.AddStripForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ListBox lstCalculations;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNextCalculation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnInsertCalculation;
        private System.Windows.Forms.Button btnDeleteCalculation;
        private System.Windows.Forms.Button btnUpdateCalculation;
        private System.Windows.Forms.TextBox txtSelectedCalculation;
    }
}