using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataLayer.Core;
using DataLayer.Data;
using DataLayer.Logic;
using DataLayer.Room;
using DataLayer.Schema;
using DataLayer.Schema.Variable;
using DataLayer.Schema.Variable.Mutable;
using DataLayer.Storage;
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
                    Description = "Otwórz drzwi",
                    Destination = @"path/to/room.room",
                    Effect = Decision.NoEffect,
                    IsVisible = true
                },
                new Decision
                {
                    Description = "Użyj kamienia przywołania [zużywa 1 klejnot dusz]",
                    Destination = @"path.room",
                    Effect = x => decisionsEntities[1].Description = "Changed",
                    IsVisible = true
                }
            };

            var room =new RoomSchema
            {
                Name = "Namke1",
                Description = "Wkroczyłeś do mrocznego pomieszczenia. Przed sobą dostrzegasz potężne dębowe drzwi.",
                Decisions = new List<DecisionSchema>
                {
                    new DecisionSchema
                    {
                        Description = "Otwórz drzwi",
                        Destination = decisionsEntities[0].Destination,
                        VisibilityRequirements = new ExpressionTrue(),
                        Effect = new ExpressionOr()
                        {
                            Args =
                            {
                                {0, new ExpressionNot()
                                {
                                    Args = 
                                    {
                                        {0, new ExpressionVariableExists("Lol")}
                                    }
                                }},
                                {1, new ExpressionAssign("test", "true") }
                            }
                        }
                            
                    },
                    new DecisionSchema
                    {
                        Description = "Użyj kamienia przywołania [zużywa 1 klejnot dusz]",
                        Destination = decisionsEntities[1].Destination,
                        VisibilityRequirements = new ExpressionTrue(),
                        Effect = new ExpressionTrue()
                    }
                }
            };

//            var mainMenu = Substitute.For<IMainMenu>();\
//            MessageBox.Show(new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.Parent.Parent.Parent.FullName);
            var storageSupervisor = new FileStorageSupervisor(@"C:\ParagraphGameData");
            var objectDataProvider = new JsonDaoProvider<StateManager>(storageSupervisor);
            var coreTranslator = new CoreTranslator();
            var roomDataProvider = new RoomDataProvider(objectDataProvider, coreTranslator);
            var entityDataProvider = new EntityDataProvider(roomDataProvider);
            var mainMenu = new MainMenu(entityDataProvider, coreTranslator);

            coreTranslator.InitializeUnit(coreTranslator.GetType().Assembly);
//            var entityMenu = Substitute.For<IEntityMenu>();
//            var entityEditorMenu = Substitute.For<IEntityEditorMenu>();

//            ICoreTranslator coreTranslator = new CoreTranslator();
//            coreTranslator.InitializeUnit(coreTranslator.GetType().Assembly);
//
//            IExpressionEditorMenu expressionEditorMenu = new ExpressionEditorMenu(coreTranslator);

//            mainMenu.StartGame(Arg.Any<string>()).Returns(entityMenu);
//            mainMenu.StartEditor(Arg.Any<string>()).Returns(entityEditorMenu);
//
//            entityMenu.DescritptionBarContent.Returns("Wkroczyłeś do mrocznego pomieszczenia. Przed sobą dostrzegasz potężne dębowe drzwi.");
//            entityMenu.GetAvailableDecisions().Returns(decisionsEntities);
//
//            entityEditorMenu.CurrentSchema.Returns(room);
//
//            entityEditorMenu.ExpressionEditorMenu.Returns(expressionEditorMenu);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main(mainMenu));
        }
    }
}
