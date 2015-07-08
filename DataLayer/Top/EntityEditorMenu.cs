using DataLayer.Core;
using DataLayer.Logic;
using DataLayer.Schema;

namespace DataLayer.Top
{
    public class EntityEditorMenu : IEntityEditorMenu
    {
        private readonly IEntityDataProvider _provider;
        private string _destination;

        public EntityEditorMenu(IEntityDataProvider provider, ICoreTranslator coreTranslator)
        {
            _provider = provider;
            ExpressionEditorMenu = new ExpressionEditorMenu(coreTranslator);
        }

        public RoomSchema CurrentSchema { get; private set; }

        public IExpressionEditorMenu ExpressionEditorMenu { get; private set; }

        public void LoadSchema(string destination)
        {
//            _provider.PerformEntityTransition(destination, _stateManager);
            _destination = destination;
            CurrentSchema = _provider.LoadRawSchema(destination);
        }

        public void SaveCurrentSchema()
        {
            _provider.SaveRawSchema(_destination, CurrentSchema);
        }
    }
}
