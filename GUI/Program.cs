using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataLayer.Logic;
using DataLayer.Schema;
using DataLayer.Top;
using NSubstitute;
using MainMenu = DataLayer.Top.MainMenu;

namespace GUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var stateManager = Substitute.For<IStateManager>();
            var dataProvider = Substitute.For<IEntityDataProvider>();
//            dataProvider.CurrentEntity.Returns(new RoomSchema{})

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main(new MainMenu(dataProvider, stateManager)));
        }
    }
}
