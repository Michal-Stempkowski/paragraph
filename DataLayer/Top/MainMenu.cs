using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Core;
using DataLayer.Logic;

namespace DataLayer.Top
{
    public class MainMenu : IMainMenu
    {
        private readonly IEntityDataProvider _provider;
        private readonly ICoreTranslator _coreTranslator;

        public MainMenu(IEntityDataProvider provider, ICoreTranslator coreTranslator)
        {
            _provider = provider;
            _coreTranslator = coreTranslator;
        }

        public void CreateNewGame(string source, string destination)
        {
            IStateManager stateManager = new StateManager();
            stateManager.SetCurrentEntity(source);
            _provider.SaveGame(destination, stateManager);
        }

        public IEntityMenu StartGame(string destination)
        {
            IStateManager stateManager = _provider.LoadGame(destination);
            var entityMenu = new EntityMenu(_provider, stateManager);
            entityMenu.PerformTransition(stateManager.GetCurrentEntity());

            return entityMenu;
        }

        public bool InitEditor(string destination)
        {
            try
            {
                _provider.CreateRoomIfIdDoesNotExist(destination);
                return true;
            }
            catch (Exception ex)
            {
                
            }

            return false;
        }

        public IEntityEditorMenu StartEditor(string destination)
        {
            var entityEditorMenu = new EntityEditorMenu(_provider, _coreTranslator);
            entityEditorMenu.LoadSchema(destination);

            return entityEditorMenu;
        }
    }
}
