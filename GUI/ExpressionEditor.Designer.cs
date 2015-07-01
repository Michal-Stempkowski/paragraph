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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Franek");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Jasiu");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Staszek", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            this._expressionTree = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // _expressionTree
            // 
            this._expressionTree.AllowDrop = true;
            this._expressionTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this._expressionTree.Location = new System.Drawing.Point(0, 0);
            this._expressionTree.Name = "_expressionTree";
            treeNode1.Name = "Node1";
            treeNode1.Text = "Franek";
            treeNode2.Name = "Node2";
            treeNode2.Text = "Jasiu";
            treeNode3.Name = "Staszek";
            treeNode3.Text = "Staszek";
            this._expressionTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3});
            this._expressionTree.ShowPlusMinus = false;
            this._expressionTree.Size = new System.Drawing.Size(284, 262);
            this._expressionTree.TabIndex = 0;
            this._expressionTree.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this._expressionTree_BeforeCollapse);
            this._expressionTree.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this._expressionTree_ItemDrag);
            this._expressionTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this._expressionTree_AfterSelect);
            this._expressionTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this._expressionTree_NodeMouseClick);
            this._expressionTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this._expressionTree_NodeMouseDoubleClick);
            this._expressionTree.DragDrop += new System.Windows.Forms.DragEventHandler(this._expressionTree_DragDrop);
            this._expressionTree.DragEnter += new System.Windows.Forms.DragEventHandler(this._expressionTree_DragEnter);
            this._expressionTree.DragOver += new System.Windows.Forms.DragEventHandler(this._expressionTree_DragOver);
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