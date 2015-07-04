using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Core;
using DataLayer.Logic;

namespace DataLayer.Top
{
    public class MainMenu : IMainMenu
    {
        private readonly IEntityDataProvider _provider;
        private readonly IStateManager _stateManager;
        private readonly ICoreTranslator _coreTranslator;

        public MainMenu(IEntityDataProvider provider, IStateManager stateManager, ICoreTranslator coreTranslator)
        {
            _provider = provider;
            _stateManager = stateManager;
            _coreTranslator = coreTranslator;
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
            var entityEditorMenu = new EntityEditorMenu(_provider, _stateManager, _coreTranslator);
            entityEditorMenu.LoadSchema(destination);

            return entityEditorMenu;
        }
    }
}
