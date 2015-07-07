using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataLayer.Schema;
using DataLayer.Schema.Variable;
using DataLayer.Schema.Variable.Mutable;
using DataLayer.Top;

namespace GUI
{
    public partial class ExpressionEditor : Form
    {
        private readonly IExpressionEditorMenu _expressionEditorMenu;
        private TreeNode _draggedNode;
        private readonly ContextMenu _nodeMenu;
        private const string RootLabel = "(root)";

        public BoolExpandableExpression Expression { get; private set; }

        public ExpressionEditor(BoolExpandableExpression expression, IExpressionEditorMenu expressionEditorMenu)
        {
            _expressionEditorMenu = expressionEditorMenu;
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
                var typeChooser = new ExpressionTypeChooser(_expressionEditorMenu);

                if (typeChooser.ShowDialog(this) == DialogResult.OK)
                {
                    var expression = typeChooser.NewExpression;
                    BuildTree(_expressionTree.SelectedNode, expression);

                    EditExpression(expression);
                }
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

        private void EditExpression(BoolExpandableExpression expression)
        {
            EditSimpleArgs(expression);

            TreeToExpression();
            ExpressionToTree(Expression);
        }

        private void TreeToExpression()
        {
            Expression = null;

            if (_expressionTree.Nodes.Count < 1 || _expressionTree.Nodes[0].Nodes.Count < 1)
            {
                return;
            }

            var root = _expressionTree.Nodes[0].Nodes[0];

            if (root == null)
            {
                return;
            }

            Expression = root.Tag as BoolExpandableExpression;
            BuildExpression(root, Expression);
        }

        private static void BuildExpression(TreeNode root, BoolExpandableExpression expression)
        {
            expression.Args.Clear();

            for (int i = 0; i < root.Nodes.Count; i++)
            {
                var child = root.Nodes[i].Tag as BoolExpandableExpression;
                expression.Args.Add(i, child);
                BuildExpression(root.Nodes[i], child);
            }
        }

        private void EditSimpleArgs(BoolExpandableExpression expression)
        {
            var simpleArgEditor = new SimpleArgEditor(expression);
            switch (expression.Name)
            {
                case "ExpressionTrue":
                case "ExpressionFalse":
                case "ExpressionOr":
                case "ExpressionAnd":
                case "ExpressionNot":
                    return;
                case "ExpressionVariableExists":
                    ServeExpressionVariableExists(simpleArgEditor);
                    break;
                case "ExpressionAssign":
                    ServeExpressionAssign(simpleArgEditor);
                    break;

                case "ExpressionIntCheck":
                    ServeExpressionIntCheck(simpleArgEditor);
                    break;
                case "ExpressionStringCheck":
                    ServeExpressionStringCheck(simpleArgEditor);
                    break;
                case "ExpressionFloatCheck":
                    ServeExpressionFloatCheck(simpleArgEditor);
                    break;
                case "ExpressionIntModify":
                    ServeExpressionIntModify(simpleArgEditor);
                    break;
                case "ExpressionFloatModify":
                    ServeExpressionFloatModify(simpleArgEditor);
                    break;
                default:
                    break;
            }

            simpleArgEditor.ShowDialog(this);

//            if (simpleArgEditor.ShowDialog(this) == DialogResult.OK)
//            {
//                expression.SimpleArgs = simpleArgEditor.Expression.SimpleArgs;
//            }
        }

        private static void ServeExpressionIntCheck(SimpleArgEditor simpleArgEditor)
        {
            simpleArgEditor.SimpleArgManagers.Add(new SimpleStringArgManager
            {
                Index = 1,
                Label = "Variable name: ",
                GetValue = (expr) =>
                {
                    var typedExpression = expr.Convert44<ExpressionIntCheck>();
                    return typedExpression.VariableName;
                },
                SetValue = (expr, val) =>
                {
                    var typedExpression = expr.Convert44<ExpressionIntCheck>();
                    typedExpression.VariableName = val;
                }
            });

            simpleArgEditor.SimpleArgManagers.Add(new SimpleEnumArgManager
            {
                Index = 2,
                Label = "Operation type: ",
                GetInstances = () => { return Enum.GetNames(typeof (CheckOperType)); },
                GetValue = (expr) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionIntCheck>();
                    return typedExpression.OperType.ToString();
                },
                SetValue = (expr, val) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionIntCheck>();
                    typedExpression.OperType = (CheckOperType) Enum.Parse(typeof (CheckOperType), val.ToString());
                }
            });

            simpleArgEditor.SimpleArgManagers.Add(new SimpleStringArgManager
            {
                Index = 3,
                Label = "Variable value: ",
                GetValue = (expr) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionIntCheck>();
                    return typedExpression.Value.ToString();
                },
                SetValue = (expr, val) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionIntCheck>();
                    typedExpression.Value = int.Parse(val);
                }
            });
        }

        private static void ServeExpressionIntModify(SimpleArgEditor simpleArgEditor)
        {
            simpleArgEditor.SimpleArgManagers.Add(new SimpleStringArgManager
            {
                Index = 1,
                Label = "Variable name: ",
                GetValue = (expr) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionIntModify>();
                    return typedExpression.VariableName;
                },
                SetValue = (expr, val) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionIntModify>();
                    typedExpression.VariableName = val;
                }
            });

            simpleArgEditor.SimpleArgManagers.Add(new SimpleEnumArgManager
            {
                Index = 2,
                Label = "Left source: ",
                GetInstances = () => { return Enum.GetNames(typeof(ExprParam.Source)); },
                GetValue = (expr) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionIntModify>();
                    return typedExpression.Left.ParamSource.ToString();
                },
                SetValue = (expr, val) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionIntModify>();
                    typedExpression.Left = new ExprParam
                    {
                        ParamSource = (ExprParam.Source) Enum.Parse(typeof (ExprParam.Source), val.ToString()),
                        Value = typedExpression.Left.Value
                    };
                }
            });

            simpleArgEditor.SimpleArgManagers.Add(new SimpleStringArgManager
            {
                Index = 3,
                Label = "Left value: ",
                GetValue = (expr) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionIntModify>();
                    return typedExpression.Left.Value;
                },
                SetValue = (expr, val) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionIntModify>();
                    typedExpression.Left = new ExprParam
                    {
                        ParamSource = typedExpression.Left.ParamSource,
                        Value = val
                    };
                }
            });

            simpleArgEditor.SimpleArgManagers.Add(new SimpleEnumArgManager
            {
                Index = 4,
                Label = "Right source: ",
                GetInstances = () => { return Enum.GetNames(typeof(ExprParam.Source)); },
                GetValue = (expr) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionIntModify>();
                    return typedExpression.Right.ParamSource.ToString();
                },
                SetValue = (expr, val) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionIntModify>();
                    typedExpression.Right = new ExprParam
                    {
                        ParamSource = (ExprParam.Source)Enum.Parse(typeof(ExprParam.Source), val.ToString()),
                        Value = typedExpression.Right.Value
                    };
                }
            });

            simpleArgEditor.SimpleArgManagers.Add(new SimpleStringArgManager
            {
                Index = 5,
                Label = "Right value: ",
                GetValue = (expr) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionIntModify>();
                    return typedExpression.Right.Value;
                },
                SetValue = (expr, val) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionIntModify>();
                    typedExpression.Right = new ExprParam
                    {
                        ParamSource = typedExpression.Right.ParamSource,
                        Value = val
                    };
                }
            });

            simpleArgEditor.SimpleArgManagers.Add(new SimpleEnumArgManager
            {
                Index = 6,
                Label = "Operation type: ",
                GetInstances = () => { return Enum.GetNames(typeof(ModifyOperType)); },
                GetValue = (expr) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionIntModify>();
                    return typedExpression.OperType.ToString();
                },
                SetValue = (expr, val) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionIntModify>();
                    typedExpression.OperType = (ModifyOperType)Enum.Parse(typeof(ModifyOperType), val.ToString());
                }
            });
        }

        private static void ServeExpressionFloatModify(SimpleArgEditor simpleArgEditor)
        {
            simpleArgEditor.SimpleArgManagers.Add(new SimpleStringArgManager
            {
                Index = 1,
                Label = "Variable name: ",
                GetValue = (expr) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionFloatModify>();
                    return typedExpression.VariableName;
                },
                SetValue = (expr, val) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionFloatModify>();
                    typedExpression.VariableName = val;
                }
            });

            simpleArgEditor.SimpleArgManagers.Add(new SimpleEnumArgManager
            {
                Index = 2,
                Label = "Left source: ",
                GetInstances = () => { return Enum.GetNames(typeof(ExprParam.Source)); },
                GetValue = (expr) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionFloatModify>();
                    return typedExpression.Left.ParamSource.ToString();
                },
                SetValue = (expr, val) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionFloatModify>();
                    typedExpression.Left = new ExprParam
                    {
                        ParamSource = (ExprParam.Source)Enum.Parse(typeof(ExprParam.Source), val.ToString()),
                        Value = typedExpression.Left.Value
                    };
                }
            });

            simpleArgEditor.SimpleArgManagers.Add(new SimpleStringArgManager
            {
                Index = 3,
                Label = "Left value: ",
                GetValue = (expr) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionFloatModify>();
                    return typedExpression.Left.Value;
                },
                SetValue = (expr, val) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionFloatModify>();
                    typedExpression.Left = new ExprParam
                    {
                        ParamSource = typedExpression.Left.ParamSource,
                        Value = val
                    };
                }
            });

            simpleArgEditor.SimpleArgManagers.Add(new SimpleEnumArgManager
            {
                Index = 4,
                Label = "Right source: ",
                GetInstances = () => { return Enum.GetNames(typeof(ExprParam.Source)); },
                GetValue = (expr) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionFloatModify>();
                    return typedExpression.Right.ParamSource.ToString();
                },
                SetValue = (expr, val) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionFloatModify>();
                    typedExpression.Right = new ExprParam
                    {
                        ParamSource = (ExprParam.Source)Enum.Parse(typeof(ExprParam.Source), val.ToString()),
                        Value = typedExpression.Right.Value
                    };
                }
            });

            simpleArgEditor.SimpleArgManagers.Add(new SimpleStringArgManager
            {
                Index = 5,
                Label = "Right value: ",
                GetValue = (expr) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionFloatModify>();
                    return typedExpression.Right.Value;
                },
                SetValue = (expr, val) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionFloatModify>();
                    typedExpression.Right = new ExprParam
                    {
                        ParamSource = typedExpression.Right.ParamSource,
                        Value = val
                    };
                }
            });

            simpleArgEditor.SimpleArgManagers.Add(new SimpleEnumArgManager
            {
                Index = 6,
                Label = "Operation type: ",
                GetInstances = () => { return Enum.GetNames(typeof(ModifyOperType)); },
                GetValue = (expr) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionFloatModify>();
                    return typedExpression.OperType.ToString();
                },
                SetValue = (expr, val) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionFloatModify>();
                    typedExpression.OperType = (ModifyOperType)Enum.Parse(typeof(ModifyOperType), val.ToString());
                }
            });
        }

        private static void ServeExpressionFloatCheck(SimpleArgEditor simpleArgEditor)
        {
            simpleArgEditor.SimpleArgManagers.Add(new SimpleStringArgManager
            {
                Index = 1,
                Label = "Variable name: ",
                GetValue = (expr) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionFloatCheck>();
                    return typedExpression.VariableName;
                },
                SetValue = (expr, val) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionFloatCheck>();
                    typedExpression.VariableName = val;
                }
            });

            simpleArgEditor.SimpleArgManagers.Add(new SimpleEnumArgManager
            {
                Index = 2,
                Label = "Operation type: ",
                GetInstances = () => { return Enum.GetNames(typeof(CheckOperType)); },
                GetValue = (expr) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionFloatCheck>();
                    return typedExpression.OperType.ToString();
                },
                SetValue = (expr, val) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionFloatCheck>();
                    typedExpression.OperType = (CheckOperType)Enum.Parse(typeof(CheckOperType), val.ToString());
                }
            });

            simpleArgEditor.SimpleArgManagers.Add(new SimpleStringArgManager
            {
                Index = 3,
                Label = "Variable value: ",
                GetValue = (expr) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionFloatCheck>();
                    return typedExpression.Value.ToString(CultureInfo.InvariantCulture);
                },
                SetValue = (expr, val) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionFloatCheck>();
                    typedExpression.Value = float.Parse(val);
                }
            });
        }

        private static void ServeExpressionStringCheck(SimpleArgEditor simpleArgEditor)
        {
            simpleArgEditor.SimpleArgManagers.Add(new SimpleStringArgManager
            {
                Index = 1,
                Label = "Variable name: ",
                GetValue = (expr) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionStringCheck>();
                    return typedExpression.VariableName;
                },
                SetValue = (expr, val) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionStringCheck>();
                    typedExpression.VariableName = val;
                }
            });

            simpleArgEditor.SimpleArgManagers.Add(new SimpleEnumArgManager
            {
                Index = 2,
                Label = "Operation type: ",
                GetInstances = () => { return Enum.GetNames(typeof(CheckOperType)); },
                GetValue = (expr) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionStringCheck>();
                    return typedExpression.OperType.ToString();
                },
                SetValue = (expr, val) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionStringCheck>();
                    typedExpression.OperType = (CheckOperType)Enum.Parse(typeof(CheckOperType), val.ToString());
                }
            });

            simpleArgEditor.SimpleArgManagers.Add(new SimpleStringArgManager
            {
                Index = 3,
                Label = "Variable value: ",
                GetValue = (expr) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionStringCheck>();
                    return typedExpression.Value;
                },
                SetValue = (expr, val) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionStringCheck>();
                    typedExpression.Value = val;
                }
            });
        }

        private static void ServeExpressionAssign(SimpleArgEditor simpleArgEditor)
        {
            simpleArgEditor.SimpleArgManagers.Add(new SimpleStringArgManager
            {
                Index = 1,
                Label = "Variable name: ",
                GetValue = (expr) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionAssign>();
                    return typedExpression.VariableName;
                },
                SetValue = (expr, val) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionAssign>();
                    typedExpression.VariableName = val;
                }
            });

            simpleArgEditor.SimpleArgManagers.Add(new SimpleStringArgManager
            {
                Index = 2,
                Label = "Variable value: ",
                GetValue = (expr) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionAssign>();
                    return typedExpression.Value;
                },
                SetValue = (expr, val) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionAssign>();
                    typedExpression.Value = val;
                }
            });
        }

        private static void ServeExpressionVariableExists(SimpleArgEditor simpleArgEditor)
        {
            simpleArgEditor.SimpleArgManagers.Add(new SimpleStringArgManager
            {
                Index = 1,
                Label = "Variable name: ",
                GetValue = (expr) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionVariableExists>();
                    return typedExpression.VariableName;
                },
                SetValue = (expr, val) =>
                {
                    var typedExpression = expr.Convert44 < ExpressionVariableExists>();
                    typedExpression.VariableName = val;
                }
            });
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

        private void _expressionTree_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void _expressionTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            _expressionTree.SelectedNode = _expressionTree.GetNodeAt(e.X, e.Y);

            if (_expressionTree.SelectedNode.Level > 0)
            {
                EditExpression(_expressionTree.SelectedNode.Tag as BoolExpandableExpression);
            }
        }
    }
}
