namespace GUI
{
    partial class DecisionEditor
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
            this._descriptionLabel = new System.Windows.Forms.Label();
            this._descriptionBox = new System.Windows.Forms.RichTextBox();
            this._destinationLabel = new System.Windows.Forms.Label();
            this._destinationBox = new System.Windows.Forms.TextBox();
            this._visibilityRequiremensLabel = new System.Windows.Forms.Label();
            this._visibilityRequirementsButton = new System.Windows.Forms.Button();
            this._effectLabel = new System.Windows.Forms.Label();
            this._effectButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.Controls.Add(this._descriptionLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._descriptionBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this._destinationLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this._destinationBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this._visibilityRequiremensLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this._visibilityRequirementsButton, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this._effectLabel, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this._effectButton, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(439, 388);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // _descriptionLabel
            // 
            this._descriptionLabel.AutoSize = true;
            this._descriptionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._descriptionLabel.Location = new System.Drawing.Point(3, 0);
            this._descriptionLabel.Name = "_descriptionLabel";
            this._descriptionLabel.Size = new System.Drawing.Size(103, 262);
            this._descriptionLabel.TabIndex = 0;
            this._descriptionLabel.Text = "Description:";
            this._descriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _descriptionBox
            // 
            this._descriptionBox.AcceptsTab = true;
            this._descriptionBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._descriptionBox.Location = new System.Drawing.Point(112, 3);
            this._descriptionBox.Name = "_descriptionBox";
            this._descriptionBox.Size = new System.Drawing.Size(324, 256);
            this._descriptionBox.TabIndex = 1;
            this._descriptionBox.Text = "";
            // 
            // _destinationLabel
            // 
            this._destinationLabel.AutoSize = true;
            this._destinationLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._destinationLabel.Location = new System.Drawing.Point(3, 262);
            this._destinationLabel.Name = "_destinationLabel";
            this._destinationLabel.Size = new System.Drawing.Size(103, 26);
            this._destinationLabel.TabIndex = 2;
            this._destinationLabel.Text = "Destination:";
            this._destinationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _destinationBox
            // 
            this._destinationBox.Dock = System.Windows.Forms.DockStyle.Top;
            this._destinationBox.Location = new System.Drawing.Point(112, 265);
            this._destinationBox.Name = "_destinationBox";
            this._destinationBox.Size = new System.Drawing.Size(324, 20);
            this._destinationBox.TabIndex = 3;
            // 
            // _visibilityRequiremensLabel
            // 
            this._visibilityRequiremensLabel.AutoSize = true;
            this._visibilityRequiremensLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._visibilityRequiremensLabel.Location = new System.Drawing.Point(3, 288);
            this._visibilityRequiremensLabel.Name = "_visibilityRequiremensLabel";
            this._visibilityRequiremensLabel.Size = new System.Drawing.Size(103, 50);
            this._visibilityRequiremensLabel.TabIndex = 4;
            this._visibilityRequiremensLabel.Text = "Visibility Requirements:";
            this._visibilityRequiremensLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _visibilityRequirementsButton
            // 
            this._visibilityRequirementsButton.AutoSize = true;
            this._visibilityRequirementsButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this._visibilityRequirementsButton.Location = new System.Drawing.Point(112, 291);
            this._visibilityRequirementsButton.Name = "_visibilityRequirementsButton";
            this._visibilityRequirementsButton.Size = new System.Drawing.Size(324, 44);
            this._visibilityRequirementsButton.TabIndex = 5;
            this._visibilityRequirementsButton.Text = "Show";
            this._visibilityRequirementsButton.UseVisualStyleBackColor = true;
            // 
            // _effectLabel
            // 
            this._effectLabel.AutoSize = true;
            this._effectLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._effectLabel.Location = new System.Drawing.Point(3, 338);
            this._effectLabel.Name = "_effectLabel";
            this._effectLabel.Size = new System.Drawing.Size(103, 50);
            this._effectLabel.TabIndex = 6;
            this._effectLabel.Text = "Effect:";
            this._effectLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _effectButton
            // 
            this._effectButton.AutoSize = true;
            this._effectButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this._effectButton.Location = new System.Drawing.Point(112, 341);
            this._effectButton.Name = "_effectButton";
            this._effectButton.Size = new System.Drawing.Size(324, 44);
            this._effectButton.TabIndex = 7;
            this._effectButton.Text = "Show";
            this._effectButton.UseVisualStyleBackColor = true;
            // 
            // DecisionEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 388);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DecisionEditor";
            this.Text = "DecisionEditor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DecisionEditor_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label _descriptionLabel;
        private System.Windows.Forms.RichTextBox _descriptionBox;
        private System.Windows.Forms.Label _destinationLabel;
        private System.Windows.Forms.TextBox _destinationBox;
        private System.Windows.Forms.Label _visibilityRequiremensLabel;
        private System.Windows.Forms.Button _visibilityRequirementsButton;
        private System.Windows.Forms.Label _effectLabel;
        private System.Windows.Forms.Button _effectButton;
    }
}