using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataLayer.Logic;
using DataLayer.Schema;
using DataLayer.Schema.Variable.Mutable;
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

            var room =new RoomSchema
            {
                Name = "Namke1",
                Description = "Some description",
                Decisions = new List<DecisionSchema>
                {
                    new DecisionSchema
                    {
                        Description = "Decision 1 description",
                        Destination = decisionsEntities[0].Destination,
                        VisibilityRequirements = new ExpressionTrue(),
                        Effect = new ExpressionAssign("test", "true")
                    },
                    new DecisionSchema
                    {
                        Description = "Second description",
                        Destination = decisionsEntities[1].Destination,
                        VisibilityRequirements = new ExpressionTrue(),
                        Effect = new ExpressionTrue()
                    }
                }
            };

            var mainMenu = Substitute.For<IMainMenu>();
            var entityMenu = Substitute.For<IEntityMenu>();
            var entityEditorMenu = Substitute.For<IEntityEditorMenu>();

            mainMenu.StartGame(Arg.Any<string>()).Returns(entityMenu);
            mainMenu.StartEditor(Arg.Any<string>()).Returns(entityEditorMenu);

            entityMenu.DescritptionBarContent.Returns("Sample description");
            entityMenu.GetAvailableDecisions().Returns(decisionsEntities);

            entityEditorMenu.CurrentSchema.Returns(room);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main(mainMenu));
        }
    }
}
