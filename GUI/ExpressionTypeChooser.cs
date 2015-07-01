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
using DataLayer.Top;

namespace GUI
{
    public partial class ExpressionTypeChooser : Form
    {
        private readonly IExpressionEditorMenu _expressionEditorMenu;

        public BoolExpandableExpression NewExpression { get; private set; }

        public ExpressionTypeChooser(IExpressionEditorMenu expressionEditorMenu)
        {
            _expressionEditorMenu = expressionEditorMenu;
            InitializeComponent();

            typeCombo.Items.Clear();
            typeCombo.Items.AddRange(_expressionEditorMenu.ExpressionNames.ToArray());
            typeCombo.SelectedIndex = 0;

            NewExpression = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewExpression = _expressionEditorMenu.CreateInstanceByName(typeCombo.SelectedItem.ToString());

            DialogResult = DialogResult.OK;
            Dispose();
        }
    }
}
