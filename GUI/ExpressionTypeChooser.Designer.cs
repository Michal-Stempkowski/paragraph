namespace GUI
{
    partial class ExpressionTypeChooser
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.typeLabel = new System.Windows.Forms.Label();
            this.typeCombo = new System.Windows.Forms.ComboBox();
            this.createButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.typeLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.typeCombo, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.createButton, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(352, 88);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // typeLabel
            // 
            this.typeLabel.AutoSize = true;
            this.typeLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.typeLabel.Location = new System.Drawing.Point(3, 0);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(346, 13);
            this.typeLabel.TabIndex = 0;
            this.typeLabel.Text = "Select new type...";
            this.typeLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // typeCombo
            // 
            this.typeCombo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.typeCombo.FormattingEnabled = true;
            this.typeCombo.Location = new System.Drawing.Point(3, 16);
            this.typeCombo.Name = "typeCombo";
            this.typeCombo.Size = new System.Drawing.Size(346, 21);
            this.typeCombo.TabIndex = 1;
            // 
            // createButton
            // 
            this.createButton.AutoSize = true;
            this.createButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.createButton.Location = new System.Drawing.Point(3, 43);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(346, 42);
            this.createButton.TabIndex = 2;
            this.createButton.Text = "createButton";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // ExpressionTypeChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 88);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ExpressionTypeChooser";
            this.Text = "ExpressionTypeChooser";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label typeLabel;
        private System.Windows.Forms.ComboBox typeCombo;
        private System.Windows.Forms.Button createButton;

    }
}