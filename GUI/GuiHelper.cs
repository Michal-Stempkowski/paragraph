using System.Windows.Forms;
using GUI.Properties;

namespace GUI
{
    public static class GuiHelper
    {
        public static bool ShowExitPromptWindow()
        {
            return MessageBox.Show(Resources.ExitApplicationPrompt, Resources.ExitApplicationTitle,
                MessageBoxButtons.YesNo) == DialogResult.Yes;
        }

        public static string ShowLoadGameFileChooser()
        {
            var file = new OpenFileDialog();

            return file.ShowDialog() == DialogResult.OK ? file.FileName : "";
        }

        public static string ShowSaveNewGameFileChooser()
        {
            var file = new SaveFileDialog();

            return file.ShowDialog() == DialogResult.OK ? file.FileName : "";
        }
    }
}