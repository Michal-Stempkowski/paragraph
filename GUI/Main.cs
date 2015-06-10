using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GUI.Properties;
using MainMenu = DataLayer.Top.MainMenu;

namespace GUI
{
    public partial class Main : Form
    {
        private readonly MainMenu _mainMenu;

        public Main()
        {
            InitializeComponent();
            _mainMenu = new MainMenu();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            var source = GuiHelper.ShowLoadGameFileChooser();

            if (String.IsNullOrWhiteSpace(source))
            {
                return;
            }

            var destination = GuiHelper.ShowSaveGameFileChooser();

            MessageBox.Show(destination);

            if (String.IsNullOrWhiteSpace(destination))
            {
                return;
            }

            _mainMenu.CreateNewGame(source, destination);
            var roomData = _mainMenu.StartGame(destination);

//            var form = newform
            Close();
        }

        private void LoadGameButton_Click(object sender, EventArgs e)
        {
            var localization = GuiHelper.ShowLoadGameFileChooser();

            if (String.IsNullOrWhiteSpace(localization))
            {
                return;
            }

            var roomData = _mainMenu.StartGame(localization);
        }

        private void EditorButton_Click(object sender, EventArgs e)
        {

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            if (GuiHelper.ShowExitPromptWindow())
            {
                Close();
            }
        }
    }
}
