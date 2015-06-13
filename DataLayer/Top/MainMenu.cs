using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Logic;

namespace DataLayer.Top
{
    public class MainMenu : IMainMenu
    {
        private readonly IEntityDataProvider _provider;
        private readonly IStateManager _stateManager;

        public MainMenu(IEntityDataProvider provider, IStateManager stateManager)
        {
            _provider = provider;
            _stateManager = stateManager;
        }

        public void CreateNewGame(string source, string destination)
        {
//            throw new NotImplementedException();
        }

        public IEntityMenu StartGame(string destination)
        {
            var entityMenu = new EntityMenu(_provider, _stateManager);
            entityMenu.PerformTransition(destination);

            return entityMenu;
        }

        public bool InitEditor(string destination)
        {
            return true;
        }

        public IEntityEditorMenu StartEditor(string destination)
        {
            var entityEditorMenu = new EntityEditorMenu(_provider, _stateManager);
            entityEditorMenu.LoadSchema(destination);

            return entityEditorMenu;
        }
    }
}
