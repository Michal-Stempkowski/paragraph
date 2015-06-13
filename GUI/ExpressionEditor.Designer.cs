namespace GUI
{
    partial class ExpressionEditor
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
            this._expressionTree = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // _expressionTree
            // 
            this._expressionTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this._expressionTree.Location = new System.Drawing.Point(0, 0);
            this._expressionTree.Name = "_expressionTree";
            this._expressionTree.Size = new System.Drawing.Size(284, 262);
            this._expressionTree.TabIndex = 0;
            // 
            // ExpressionEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this._expressionTree);
            this.Name = "ExpressionEditor";
            this.Text = "ExpressionEditor";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView _expressionTree;
    }
}