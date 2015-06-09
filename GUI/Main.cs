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

namespace GUI
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
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

            if (String.IsNullOrWhiteSpace(destination))
            {
                return;
            }
        }

        private void LoadGameButton_Click(object sender, EventArgs e)
        {

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
