namespace AddStrip
{
    partial class frmAddStrip
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
            this.mnuAddStrip = new System.Windows.Forms.MenuStrip();
            this.tlstrpFileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tlstrpItemNew = new System.Windows.Forms.ToolStripMenuItem();
            this.tlstrpItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.tlstrpItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tlstrpItemSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlstrpItemPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tlstrpItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.lstCalculations = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNextCalculation = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnInsertCalculation = new System.Windows.Forms.Button();
            this.btnDeleteCalculation = new System.Windows.Forms.Button();
            this.btnUpdateCalculation = new System.Windows.Forms.Button();
            this.txtSelectedCalculation = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.mnuAddStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuAddStrip
            // 
            this.mnuAddStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuAddStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlstrpFileMenu});
            this.mnuAddStrip.Location = new System.Drawing.Point(0, 0);
            this.mnuAddStrip.Name = "mnuAddStrip";
            this.mnuAddStrip.Size = new System.Drawing.Size(583, 28);
            this.mnuAddStrip.TabIndex = 0;
            this.mnuAddStrip.Text = "mnuFile";
            // 
            // tlstrpFileMenu
            // 
            this.tlstrpFileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlstrpItemNew,
            this.tlstrpItemOpen,
            this.tlstrpItemSave,
            this.tlstrpItemSaveAs,
            this.toolStripSeparator1,
            this.tlstrpItemPrint,
            this.toolStripSeparator2,
            this.tlstrpItemExit});
            this.tlstrpFileMenu.Name = "tlstrpFileMenu";
            this.tlstrpFileMenu.Size = new System.Drawing.Size(44, 24);
            this.tlstrpFileMenu.Text = "File";
            // 
            // tlstrpItemNew
            // 
            this.tlstrpItemNew.Name = "tlstrpItemNew";
            this.tlstrpItemNew.Size = new System.Drawing.Size(181, 26);
            this.tlstrpItemNew.Text = "New";
            this.tlstrpItemNew.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // tlstrpItemOpen
            // 
            this.tlstrpItemOpen.Name = "tlstrpItemOpen";
            this.tlstrpItemOpen.Size = new System.Drawing.Size(181, 26);
            this.tlstrpItemOpen.Text = "Open...";
            this.tlstrpItemOpen.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // tlstrpItemSave
            // 
            this.tlstrpItemSave.Name = "tlstrpItemSave";
            this.tlstrpItemSave.Size = new System.Drawing.Size(181, 26);
            this.tlstrpItemSave.Text = "Save...";
            this.tlstrpItemSave.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // tlstrpItemSaveAs
            // 
            this.tlstrpItemSaveAs.Name = "tlstrpItemSaveAs";
            this.tlstrpItemSaveAs.Size = new System.Drawing.Size(181, 26);
            this.tlstrpItemSaveAs.Text = "Save As...";
            this.tlstrpItemSaveAs.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(178, 6);
            // 
            // tlstrpItemPrint
            // 
            this.tlstrpItemPrint.Name = "tlstrpItemPrint";
            this.tlstrpItemPrint.Size = new System.Drawing.Size(181, 26);
            this.tlstrpItemPrint.Text = "Print";
            this.tlstrpItemPrint.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(178, 6);
            // 
            // tlstrpItemExit
            // 
            this.tlstrpItemExit.Name = "tlstrpItemExit";
            this.tlstrpItemExit.Size = new System.Drawing.Size(181, 26);
            this.tlstrpItemExit.Text = "Exit";
            this.tlstrpItemExit.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
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
            this.label1.Size = new System.Drawing.Size(201, 38);
            this.label1.TabIndex = 2;
            this.label1.Text = "Enter your calculations in the text box below:";
            // 
            // txtNextCalculation
            // 
            this.txtNextCalculation.Location = new System.Drawing.Point(273, 73);
            this.txtNextCalculation.Name = "txtNextCalculation";
            this.txtNextCalculation.Size = new System.Drawing.Size(183, 22);
            this.txtNextCalculation.TabIndex = 3;
            this.txtNextCalculation.TextChanged += new System.EventHandler(this.txtNewCalculation_TextChanged);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(266, 180);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(201, 38);
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
            this.panel1.Size = new System.Drawing.Size(279, 98);
            this.panel1.TabIndex = 5;
            // 
            // btnInsertCalculation
            // 
            this.btnInsertCalculation.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInsertCalculation.Location = new System.Drawing.Point(185, 55);
            this.btnInsertCalculation.Name = "btnInsertCalculation";
            this.btnInsertCalculation.Size = new System.Drawing.Size(79, 30);
            this.btnInsertCalculation.TabIndex = 9;
            this.btnInsertCalculation.Text = "Insert";
            this.btnInsertCalculation.UseVisualStyleBackColor = true;
            this.btnInsertCalculation.Click += new System.EventHandler(this.btnInsertCalculation_Click);
            // 
            // btnDeleteCalculation
            // 
            this.btnDeleteCalculation.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteCalculation.Location = new System.Drawing.Point(99, 55);
            this.btnDeleteCalculation.Name = "btnDeleteCalculation";
            this.btnDeleteCalculation.Size = new System.Drawing.Size(79, 30);
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
            this.btnUpdateCalculation.Size = new System.Drawing.Size(79, 30);
            this.btnUpdateCalculation.TabIndex = 7;
            this.btnUpdateCalculation.Text = "Update";
            this.btnUpdateCalculation.UseVisualStyleBackColor = true;
            this.btnUpdateCalculation.Click += new System.EventHandler(this.btnUpdateCalculation_Click);
            // 
            // txtSelectedCalculation
            // 
            this.txtSelectedCalculation.Location = new System.Drawing.Point(49, 15);
            this.txtSelectedCalculation.Name = "txtSelectedCalculation";
            this.txtSelectedCalculation.Size = new System.Drawing.Size(183, 22);
            this.txtSelectedCalculation.TabIndex = 6;
            this.txtSelectedCalculation.TextChanged += new System.EventHandler(this.txtSelectedCalculation_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(273, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 17);
            this.label3.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(400, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 17);
            this.label4.TabIndex = 7;
            // 
            // frmAddStrip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 349);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNextCalculation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstCalculations);
            this.Controls.Add(this.mnuAddStrip);
            this.MainMenuStrip = this.mnuAddStrip;
            this.Name = "frmAddStrip";
            this.Text = "AddStripForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddStripForm_FormClosing);
            this.Load += new System.EventHandler(this.AddStripForm_Load);
            this.mnuAddStrip.ResumeLayout(false);
            this.mnuAddStrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuAddStrip;
        private System.Windows.Forms.ToolStripMenuItem tlstrpFileMenu;
        private System.Windows.Forms.ToolStripMenuItem tlstrpItemNew;
        private System.Windows.Forms.ToolStripMenuItem tlstrpItemOpen;
        private System.Windows.Forms.ToolStripMenuItem tlstrpItemSave;
        private System.Windows.Forms.ToolStripMenuItem tlstrpItemSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tlstrpItemPrint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tlstrpItemExit;
        private System.Windows.Forms.ListBox lstCalculations;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNextCalculation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnInsertCalculation;
        private System.Windows.Forms.Button btnDeleteCalculation;
        private System.Windows.Forms.Button btnUpdateCalculation;
        private System.Windows.Forms.TextBox txtSelectedCalculation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}