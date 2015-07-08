using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataLayer.Top;
using GUI.Properties;
using NSubstitute;
using MainMenu = DataLayer.Top.MainMenu;

namespace GUI
{
    public partial class Main : Form
    {
        private readonly IMainMenu _mainMenu;

        public Main(IMainMenu mainMenu)
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

            var form = new Entity(entityMenu, this);
            form.Show();
            Hide();
        }

        private string CreateNewGame()
        {
            var source = GuiHelper.ShowLoadScenarioFileChooser();

            if (String.IsNullOrWhiteSpace(source))
            {
                return "";
            }

            var destination = GuiHelper.ShowSaveNewGameFileChooser();

            if (String.IsNullOrWhiteSpace(destination))
            {
                return destination;
            }

            _mainMenu.CreateNewGame(source, destination);
            return destination;
        }

        private void LoadGameButton_Click(object sender, EventArgs e)
        {
            LoadGame(GuiHelper.ShowLoadGameFileChooser());
        }

        private void EditorButton_Click(object sender, EventArgs e)
        {
//            var entityMenu = _mainMenu.StartGame("");
//            var entity = new Entity(entityMenu, this);
//
//            entity.Show();
//            Hide();
            var destination = GuiHelper.ShowEditorFileChooser();

            if (String.IsNullOrWhiteSpace(destination) ||
                !_mainMenu.InitEditor(destination))
            {
//                GuiHelper.ShowWarning(Resources.Main_EditorButton_Click_Invalid_path_or_read_error);
                return;
            }

            var entityEditorMenu = _mainMenu.StartEditor(destination);

            var entityEditor = new EntityEditor(entityEditorMenu, this);
            entityEditor.Show();

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
