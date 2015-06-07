using DataLayer.Logic;
using DataLayer.Schema;

namespace DataLayer.Data
{
    public interface IDaoProvider
    {
        RoomSchema ReadRoom(string pathToRoom);
        void WriteRoom(string pathForNewRoomRoom, RoomSchema room);
        void WriteStateManager(string path, IStateManager stateManager);
        IStateManager ReadStateManager(string path);
    }
}