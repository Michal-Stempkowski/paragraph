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
    public partial class DecisionEditor : Form
    {
        private readonly IEntityEditorMenu _entityEditorMenu;
        private readonly DecisionSchema _decision;

        public DecisionEditor(IEntityEditorMenu entityEditorMenu, DecisionSchema decision)
        {
            _entityEditorMenu = entityEditorMenu;
            _decision = decision;
            InitializeComponent();

            DecisionToGui();
        }

        private void DecisionToGui()
        {
            _descriptionBox.Text = _decision.Description;
            _destinationBox.Text = _decision.Destination;


            _visibilityRequirementsButton.Tag = _decision.VisibilityRequirements;
            _visibilityRequirementsButton.Click += VisibilityRequirementsButtonOnClick;

            _effectButton.Tag = _decision.Effect;
        }

        private void VisibilityRequirementsButtonOnClick(object sender, EventArgs eventArgs)
        {
            _decision.VisibilityRequirements = ShowExpressionEditor(_decision.VisibilityRequirements);
        }

        private BoolExpandableExpression ShowExpressionEditor(BoolExpandableExpression expression)
        {
            var expressionEditor = new ExpressionEditor(expression);

            expressionEditor.ShowDialog(this);
            
            return expressionEditor.Expression;
        }

        private void _effectButton_Click(object sender, EventArgs e)
        {
            _decision.Effect = ShowExpressionEditor(_decision.Effect);
        }

        private void GuiToDecision()
        {
            _decision.Description = _descriptionBox.Text;
            _decision.Destination = _destinationBox.Text;
            _decision.VisibilityRequirements = _visibilityRequirementsButton.Tag as BoolExpandableExpression;
            _decision.Effect = _effectButton.Tag as BoolExpandableExpression;
        }

        private void DecisionEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            GuiToDecision();
        }
    }
}
