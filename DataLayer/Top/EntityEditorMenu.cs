using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Logic;
using DataLayer.Schema;

namespace DataLayer.Top
{
    public class EntityEditorMenu : IEntityEditorMenu
    {
        private readonly IEntityDataProvider _provider;
        private readonly IStateManager _stateManager;

        public EntityEditorMenu(IEntityDataProvider provider, IStateManager stateManager)
        {
            _provider = provider;
            _stateManager = stateManager;
        }

        public RoomSchema CurrentSchema { get; private set; }

        public void LoadSchema(string destination)
        {
            throw new NotImplementedException();
        }
    }
}
