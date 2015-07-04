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
    }
}