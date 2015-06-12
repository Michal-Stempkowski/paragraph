namespace GUI
{
    partial class Entity
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
            this._textLayout = new System.Windows.Forms.TableLayoutPanel();
            this._nameTextbox = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.decisionPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.addNewDecisionButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this._textLayout.SuspendLayout();
            this.decisionPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this._textLayout, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.decisionPanel, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(260, 238);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // _textLayout
            // 
            this._textLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._textLayout.ColumnCount = 1;
            this._textLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._textLayout.Controls.Add(this._nameTextbox, 0, 0);
            this._textLayout.Controls.Add(this.textBox1, 0, 1);
            this._textLayout.Location = new System.Drawing.Point(3, 3);
            this._textLayout.Name = "_textLayout";
            this._textLayout.RowCount = 2;
            this._textLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._textLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._textLayout.Size = new System.Drawing.Size(257, 160);
            this._textLayout.TabIndex = 0;
            // 
            // _nameTextbox
            // 
            this._nameTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._nameTextbox.Location = new System.Drawing.Point(3, 3);
            this._nameTextbox.Name = "_nameTextbox";
            this._nameTextbox.Size = new System.Drawing.Size(251, 20);
            this._nameTextbox.TabIndex = 0;
            this._nameTextbox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox1
            // 
            this.textBox1.AcceptsReturn = true;
            this.textBox1.AcceptsTab = true;
            this.textBox1.AllowDrop = true;
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(3, 29);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(251, 131);
            this.textBox1.TabIndex = 1;
            // 
            // decisionPanel
            // 
            this.decisionPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.decisionPanel.Controls.Add(this.addNewDecisionButton);
            this.decisionPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.decisionPanel.Location = new System.Drawing.Point(3, 169);
            this.decisionPanel.Name = "decisionPanel";
            this.decisionPanel.Size = new System.Drawing.Size(257, 66);
            this.decisionPanel.TabIndex = 1;
            // 
            // addNewDecisionButton
            // 
            this.addNewDecisionButton.Location = new System.Drawing.Point(3, 3);
            this.addNewDecisionButton.Name = "addNewDecisionButton";
            this.addNewDecisionButton.Size = new System.Drawing.Size(75, 23);
            this.addNewDecisionButton.TabIndex = 0;
            this.addNewDecisionButton.Text = "Add new decision";
            this.addNewDecisionButton.UseVisualStyleBackColor = true;
            // 
            // Entity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Entity";
            this.Text = "Entity";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this._onClosing);
            this.Load += new System.EventHandler(this.Entity_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this._textLayout.ResumeLayout(false);
            this._textLayout.PerformLayout();
            this.decisionPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel _textLayout;
        private System.Windows.Forms.TextBox _nameTextbox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.FlowLayoutPanel decisionPanel;
        private System.Windows.Forms.Button addNewDecisionButton;

    }
}