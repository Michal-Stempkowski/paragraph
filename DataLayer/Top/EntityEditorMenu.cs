using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Core;
using DataLayer.Logic;
using DataLayer.Schema;

namespace DataLayer.Top
{
    public class EntityEditorMenu : IEntityEditorMenu
    {
        private readonly IEntityDataProvider _provider;
        private readonly IStateManager _stateManager;

        public EntityEditorMenu(IEntityDataProvider provider, IStateManager stateManager, ICoreTranslator coreTranslator)
        {
            _provider = provider;
            _stateManager = stateManager;
            ExpressionEditorMenu = new ExpressionEditorMenu(coreTranslator);
        }

        public RoomSchema CurrentSchema { get; private set; }

        public IExpressionEditorMenu ExpressionEditorMenu { get; private set; }

        public void LoadSchema(string destination)
        {
//            _provider.PerformEntityTransition(destination, _stateManager);
            CurrentSchema = _provider.LoadRawSchema(destination);
        }
    }
}
