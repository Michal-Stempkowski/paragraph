using DataLayer.Schema;

namespace DataLayer.Top
{
    public interface IEntityEditorMenu
    {
        RoomSchema CurrentSchema { get; }
        IExpressionEditorMenu ExpressionEditorMenu { get; }
        void LoadSchema(string destination);
        void SaveCurrentSchema();
    }
}