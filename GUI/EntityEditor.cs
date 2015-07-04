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
using Microsoft.CSharp.RuntimeBinder;

namespace GUI
{
    public partial class EntityEditor : Form
    {
        private readonly IEntityEditorMenu _entityEditorMenu;
        private readonly Main _main;

        public EntityEditor(IEntityEditorMenu entityEditorMenu, Main main)
        {
            _entityEditorMenu = entityEditorMenu;
            _main = main;
            InitializeComponent();

            ReloadGui();
        }

        private void ReloadGui()
        {
            toolTipHelper.RemoveAll();

            var roomSchema = _entityEditorMenu.CurrentSchema;

            nameBox.Text = roomSchema.Name;
            descriptionBox.Text = roomSchema.Description;

            decisionPanel.Controls.Clear();
            decisionPanel.Controls.Add(addNewDecisionButton);

            roomSchema.Decisions.ForEach(AddDecisionButton);
        }

        private void AddDecisionButton(DecisionSchema decision)
        {
            var button = new Button
            {
                AutoSize = true,
                Text = decision.Description,
                Tag = decision
            };

            button.Click += HandleDecisionButtonClick;

            decisionPanel.Controls.Add(button);
            toolTipHelper.SetToolTip(button, decision.Destination);
            decisionPanel.SetFlowBreak(button, true);
        }

        private void HandleDecisionButtonClick(object sender, EventArgs args)
        {
            var self = sender as Button;

            var decision = self.Tag as DecisionSchema;

            var index = _entityEditorMenu.CurrentSchema.Decisions.IndexOf(decision);

            switch (ModifierKeys)
            {
                case Keys.Control:
                    if (ConfirmRoomDisposal())
                    {
                        PerformGoto(decision.Destination);
                    }
                    break;
                case Keys.Alt:
                    _entityEditorMenu.CurrentSchema.Decisions.RemoveAt(index);
                    break;
                default:
                    var decisionEditor = new DecisionEditor(_entityEditorMenu, decision);

                    decisionEditor.ShowDialog(this);
                    _entityEditorMenu.SaveCurrentSchema();
                    _entityEditorMenu.CurrentSchema.Decisions.RemoveAt(index);
                    _entityEditorMenu.CurrentSchema.Decisions.Insert(index, decisionEditor.Decision);
                    break;
            }

            ReloadGui();
        }

        private void PerformGoto(string destination)
        {
            _entityEditorMenu.LoadSchema(destination);
        }

        private bool ConfirmRoomDisposal()
        {
            switch (GuiHelper.ShowPromptWindow("Do you want to save room?"))
            {
                case DialogResult.Yes:
                    _entityEditorMenu.SaveCurrentSchema();
                    return true;
                case DialogResult.No:
                    return true;
                case DialogResult.Cancel:
                    return false;
                default:
                    throw new NotImplementedException();
            }
        }

        private void EntityEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!ConfirmRoomDisposal())
            {
                e.Cancel = true;
                return;
            }

            _main.Show();
        }

        private void addNewDecisionButton_Click(object sender, EventArgs e)
        {
            _entityEditorMenu.CurrentSchema.Decisions.Add(new DecisionSchema
            {
                Description = "Empty description",
                Destination = @"//INVALID_DESTINATION",
                Effect = new ExpressionTrue(),
                VisibilityRequirements = new ExpressionTrue()
            });

            ReloadGui();
        }
    }
}
