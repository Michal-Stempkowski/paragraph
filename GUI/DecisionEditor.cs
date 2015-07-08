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
        public readonly DecisionSchema Decision;

        public DecisionEditor(IEntityEditorMenu entityEditorMenu, DecisionSchema decision)
        {
            _entityEditorMenu = entityEditorMenu;
            Decision = decision;
            InitializeComponent();
            _visibilityRequirementsButton.Click += VisibilityRequirementsButtonOnClick;

            DecisionToGui();
        }

        private void DecisionToGui()
        {
            _descriptionBox.Text = Decision.Description;
            _destinationBox.Text = Decision.Destination;


            _visibilityRequirementsButton.Tag = Decision.VisibilityRequirements;
//            _visibilityRequirementsButton.Click += VisibilityRequirementsButtonOnClick;

            _effectButton.Tag = Decision.Effect;
        }

        private void VisibilityRequirementsButtonOnClick(object sender, EventArgs eventArgs)
        {
            try
            {
                GuiToDecision();
                Decision.VisibilityRequirements = ShowExpressionEditor(Decision.VisibilityRequirements);
                DecisionToGui();
            }
            catch (Exception ex)
            {
                GuiHelper.ShowWarning(ex.Message);
            }
           
        }

        private BoolExpandableExpression ShowExpressionEditor(BoolExpandableExpression expression)
        {
            var expressionEditor = new ExpressionEditor(expression, _entityEditorMenu.ExpressionEditorMenu);

            expressionEditor.ShowDialog(this);
            
            return expressionEditor.Expression;
        }

        private void _effectButton_Click(object sender, EventArgs e)
        {
            GuiToDecision();
            Decision.Effect = ShowExpressionEditor(Decision.Effect);
            DecisionToGui();
        }

        private void GuiToDecision()
        {
            Decision.Description = _descriptionBox.Text;
            Decision.Destination = _destinationBox.Text;
            Decision.VisibilityRequirements = _visibilityRequirementsButton.Tag as BoolExpandableExpression;
            Decision.Effect = _effectButton.Tag as BoolExpandableExpression;
        }

        private void DecisionEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            GuiToDecision();
        }

        private void DecisionEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
//            GuiToDecision();
        }
    }
}
