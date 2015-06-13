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
            switch (ModifierKeys)
            {
                case Keys.Control:
                    throw new NotImplementedException();
                case Keys.Alt:
                    throw new NotImplementedException();
            }

            var decision = self.Tag as DecisionSchema;

            var decisionEditor = new DecisionEditor(_entityEditorMenu, decision);

            decisionEditor.ShowDialog(this);

            ReloadGui();
        }

        private void EntityEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            _main.Show();
        }
    }
}
