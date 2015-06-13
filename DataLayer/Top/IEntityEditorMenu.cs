using DataLayer.Schema;

namespace DataLayer.Top
{
    public interface IEntityEditorMenu
    {
        RoomSchema CurrentSchema { get; }
        void LoadSchema(string destination);
    }
}