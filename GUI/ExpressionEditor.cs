using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataLayer.Schema;

namespace GUI
{
    public partial class ExpressionEditor : Form
    {
        private TreeNode _draggedNode;
        private ContextMenu _nodeMenu;
        private const string RootLabel = "(root)";

        public BoolExpandableExpression Expression { get; private set; }

        public ExpressionEditor(BoolExpandableExpression expression)
        {
            Expression = expression;
            _draggedNode = null;

            InitializeComponent();

            ExpressionToTree(Expression);

            _nodeMenu = CreateContextMenu();
        }

        private ContextMenu CreateContextMenu()
        {
            var createItem = new MenuItem("Create child", (sender, args) =>
            {
                        
            });

            var deleteItem = new MenuItem("Delete", (sender, args) => _expressionTree.SelectedNode.Remove());

            var moveUpItem = new MenuItem("Move up", (sender, args) =>
            {
                var node = _expressionTree.SelectedNode;
                var parent = node.Parent;
                int index = node.Index;

                if (index <= 0) return;

                parent.Nodes.RemoveAt(index);
                parent.Nodes.Insert(index - 1, node);
            });

            var moveDownItem = new MenuItem("Move down", (sender, args) =>
            {
                var node = _expressionTree.SelectedNode;
                var parent = node.Parent;
                int index = node.Index;

                if (index >= parent.Nodes.Count - 1) return;

                parent.Nodes.RemoveAt(index);
                parent.Nodes.Insert(index + 1, node); 
            });

            var contextMenu = new ContextMenu(
                new[]
                {
                    createItem,
                    deleteItem,
                    moveUpItem,
                    moveDownItem
                });

            contextMenu.Popup += (sender, args) =>
            {
                var selectedNode = _expressionTree.SelectedNode;
                deleteItem.Enabled = _expressionTree.SelectedNode.Level > 0;
                moveUpItem.Enabled = selectedNode.Level > 0 && selectedNode.Parent.FirstNode != selectedNode;
                moveDownItem.Enabled = selectedNode.Level > 0 && selectedNode.Parent.LastNode != selectedNode;
            };

            return contextMenu;
        }

        private void ExpressionToTree(BoolExpandableExpression expression)
        {
            var root = new TreeNode()
            {
                Text = RootLabel,
                Tag = null
            };

            BuildTree(root, Expression);

            _expressionTree.Nodes.Clear();
            _expressionTree.Nodes.Add(root);
            _expressionTree.ExpandAll();
        }

        private void BuildTree(TreeNode parrentNode, BoolExpandableExpression expression)
        {
            if (expression == null) return;

            var thisNode = new TreeNode()
            {
                Text = expression.ToString(),
                Tag = expression
            };

            parrentNode.Nodes.Add(thisNode);

            foreach (var arg in expression.Args
                .OrderBy(x => x.Key)
                .Select(x => x.Value))
            {
                BuildTree(thisNode, arg);
            }
        }

        private void _expressionTree_ItemDrag(object sender, ItemDragEventArgs e)
        {
            _draggedNode = e.Item as TreeNode;
            DoDragDrop(e.Item, 
                _draggedNode != null && _draggedNode.Level > 0 ? 
                DragDropEffects.Move : 
                DragDropEffects.None);
        }

        private void _expressionTree_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void _expressionTree_DragOver(object sender, DragEventArgs e)
        {
            Point p = _expressionTree.PointToClient(new Point(e.X, e.Y));
            TreeNode node = _expressionTree.GetNodeAt(p.X, p.Y);

            _expressionTree.SelectedNode = node;
        }

        private void _expressionTree_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void _expressionTree_DragDrop(object sender, DragEventArgs e)
        {
            var source = _draggedNode;
            var destination = _expressionTree.SelectedNode;

            MoveNode(source, destination);
        }

        private void MoveNode(TreeNode source, TreeNode destination)
        {
            if (source == null ||
                destination == null ||
                source == destination ||
                source.Level == 0)
            {
                return;
            }

            var sourceCopy = DeepCopyNode(source);

            if (destination.Level == 0)
            {
                destination.Nodes.Clear();
            }

            destination.Nodes.Add(sourceCopy);
            destination.ExpandAll();

            source.Remove();
        }

        public TreeNode DeepCopyNode(TreeNode sourceNode)
        {
            var copy = sourceNode.Clone() as TreeNode;
            copy.Nodes.Clear();
            foreach (TreeNode node in sourceNode.Nodes)
            {
                copy.Nodes.Add(DeepCopyNode(node));
            }

            return copy;
        }

        private void _expressionTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            _expressionTree.SelectedNode = _expressionTree.GetNodeAt(e.X, e.Y);
            if (e.Button == MouseButtons.Right && _expressionTree.SelectedNode != null)
            {
                _nodeMenu.Show(this, e.Location);
            }
        }
    }
}
