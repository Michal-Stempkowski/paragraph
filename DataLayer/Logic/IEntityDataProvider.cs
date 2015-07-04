using DataLayer.Schema;

namespace DataLayer.Logic
{
    public interface IEntityDataProvider
    {
        bool PerformEntityTransition(string entityPath, IStateManager stateManager);
        IEntity CurrentEntity { get; }
        void CreateRoomIfIdDoesNotExist(string destination);
        RoomSchema LoadRawSchema(string destination);
        void SaveRawSchema(string destination, RoomSchema roomSchema);
        IStateManager LoadGame(string destination);
        void SaveGame(string destination, IStateManager stateManager);
    }
}