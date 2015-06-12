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
using NSubstitute;
using MainMenu = DataLayer.Top.MainMenu;

namespace GUI
{
    public partial class Main : Form
    {
        private readonly MainMenu _mainMenu;

        public Main(MainMenu mainMenu)
        {
            _mainMenu = mainMenu;
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            LoadGame(CreateNewGame());
        }

        private void LoadGame(string destination)
        {
            if (String.IsNullOrWhiteSpace(destination))
            {
                return;
            }

            var entityMenu = _mainMenu.StartGame(destination);

            var form = new Entity(entityMenu, this, new PresenterEntityCreator());
            form.Show();
            Hide();
        }

        private string CreateNewGame()
        {
            var source = GuiHelper.ShowLoadGameFileChooser();

            if (String.IsNullOrWhiteSpace(source))
            {
                return "";
            }

            var destination = GuiHelper.ShowSaveNewGameFileChooser();

            if (String.IsNullOrWhiteSpace(destination))
            {
                return destination;
            }

            MessageBox.Show(destination);

            _mainMenu.CreateNewGame(source, destination);
            return destination;
        }

        private void LoadGameButton_Click(object sender, EventArgs e)
        {
            LoadGame(GuiHelper.ShowLoadGameFileChooser());
        }

        private void EditorButton_Click(object sender, EventArgs e)
        {
            var entityMenu = _mainMenu.StartGame("");
            var entity = new Entity(entityMenu, this, new EditorEntityCreator());

            entity.Show();
            Hide();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !GuiHelper.ShowExitPromptWindow();
        }
    }
}
