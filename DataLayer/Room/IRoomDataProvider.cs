using DataLayer.Logic;
using DataLayer.Schema;

namespace DataLayer.Room
{
    public interface IRoomDataProvider
    {
        IEntity LoadRoom(string roomPath, IStateManager stateManager);
        void CreateIfNeeded(string destination);
        RoomSchema LoadRoomSchema(string destination);
        void SaveRoomSchema(string destination, RoomSchema roomSchema);
    }
}