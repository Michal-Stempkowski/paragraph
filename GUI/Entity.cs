using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataLayer.Logic;
using DataLayer.Top;

namespace GUI
{
    public partial class Entity : Form
    {
        private readonly IEntityMenu _entityMenu; 
        private readonly Main _mainMenuForm;

        public Entity(IEntityMenu entityMenu, Main mainMenuForm)
        {
            _entityMenu = entityMenu;
            _mainMenuForm = mainMenuForm;
            InitializeComponent();

            LoadEntityData();
        }

        private void LoadEntityData()
        {
            descriptionBox.Text = _entityMenu.DescritptionBarContent;

            decisionPanel.Controls.Clear();

            foreach (var button in _entityMenu.GetAvailableDecisions().Select(decision => new Button
            {
                Text = decision.Description,
                Visible = decision.IsVisible,
                Tag = decision,
                AutoSize = true
            }))
            {
                button.Click += (sender, args) =>
                {
                    var decision = (sender as Button).Tag as IDecision;
                    decision.Effect(_entityMenu.StateManager);
                    _entityMenu.PerformTransition(decision.Destination);

                    LoadEntityData();
                };
                decisionPanel.Controls.Add(button);
            }
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Entity_Load(object sender, EventArgs e)
        {

        }

        private void _onClosing(object sender, FormClosingEventArgs e)
        {
            switch (GuiHelper.ShowPromptWindow("Do yuo want to save game?"))
            {
                case DialogResult.Yes:
                    var path = GuiHelper.ShowSaveNewGameFileChooser();
                    _entityMenu.SaveGame(path);
                    break;
                case DialogResult.No:
                    break;
                case DialogResult.Cancel:
                    e.Cancel = true;
                    return;
                default:
                    throw new NotImplementedException();
            }
            _mainMenuForm.Show();
        }
    }
}
