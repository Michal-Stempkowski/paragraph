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
        public static DialogResult ShowPromptWindow(string prompt)
        {
            return MessageBox.Show(prompt, Resources.GuiHelper_ShowPromptWindow_Prompt, MessageBoxButtons.YesNoCancel);
        }

        public static string ShowLoadGameFileChooser()
        {
            var file = new OpenFileDialog()
            {
                Filter = Resources.GuiHelper_save_files
            };

            return file.ShowDialog() == DialogResult.OK ? file.FileName : "";
        }

        public static string ShowSaveNewGameFileChooser()
        {
            var file = new SaveFileDialog()
            {
                Filter = Resources.GuiHelper_save_files
            };

            return file.ShowDialog() == DialogResult.OK ? file.FileName : "";
        }

        public static string ShowEditorFileChooser()
        {
            var file = new OpenFileDialog
            {
                CheckFileExists = false, 
                Title = Resources.GuiHelper_ShowEditorFileChooser_Select_existing_or_create_new_file,
                Filter = Resources.GuiHelper_room_files
            };

            return file.ShowDialog() == DialogResult.OK ? file.FileName : "";
        }

        public static DialogResult ShowErrorDialog(IWin32Window owner, string text)
        {
            return MessageBox.Show(owner, text, Resources.Main_EditorButton_Click_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static string ShowLoadScenarioFileChooser()
        {
            var file = new OpenFileDialog()
            {
                Filter = Resources.GuiHelper_room_files
            };

            return file.ShowDialog() == DialogResult.OK ? file.FileName : "";
        }
    }
}