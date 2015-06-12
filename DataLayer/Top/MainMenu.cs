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

        public EntityMenu StartGame(string destination)
        {
            var entityMenu = new EntityMenu(_provider, _stateManager);
            entityMenu.PerformTransition(destination);

            return entityMenu;
        }
    }
}
