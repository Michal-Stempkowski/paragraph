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
//            var stateManager = Substitute.For<IStateManager>();
//            var dataProvider = Substitute.For<IEntityDataProvider>();
//            dataProvider.

            List<IDecision> decisionsEntities = null;

            decisionsEntities = new List<IDecision>
            {
                new Decision
                {
                    Description = "Description1",
                    Destination = @"path/to/room.room",
                    Effect = Decision.NoEffect,
                    IsVisible = true
                },
                new Decision
                {
                    Description = "Description2",
                    Destination = @"path.room",
                    Effect = x => decisionsEntities[1].Description = "Changed",
                    IsVisible = true
                }
            };

            var mainMenu = Substitute.For<IMainMenu>();
            var entityMenu = Substitute.For<IEntityMenu>();

            mainMenu.StartGame(Arg.Any<string>()).Returns(entityMenu);

            entityMenu.DescritptionBarContent.Returns("Sample description");
            entityMenu.GetAvailableDecisions().Returns(decisionsEntities);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main(mainMenu));
        }
    }
}
