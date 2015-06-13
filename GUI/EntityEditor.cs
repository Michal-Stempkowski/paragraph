using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

            toolTipHelper.RemoveAll();

            var roomSchema = _entityEditorMenu.CurrentSchema;

            nameBox.Text = roomSchema.Name;
            descriptionBox.Text = roomSchema.Description;

            decisionPanel.Controls.Clear();
            decisionPanel.Controls.Add(addNewDecisionButton);

            foreach (var decision in roomSchema.Decisions)
            {
                var button = new Button
                {
                    Text = decision.Description,
                    
                };

                button.Click += (sender, args) =>
                {
                    switch (ModifierKeys)
                    {
                        case Keys.Control:
                            throw new NotImplementedException();
                        case Keys.Alt:
                            throw new NotImplementedException();
                    }
                };

                decisionPanel.Controls.Add(button);
                toolTipHelper.SetToolTip(button, decision.Destination);
                decisionPanel.SetFlowBreak(button, true);
            }
        }

        private void EntityEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            _main.Show();
        }
    }
}
