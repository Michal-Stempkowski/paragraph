using DataLayer.Logic;

namespace DataLayer.Room
{
    public interface IRoomDataProvider
    {
        IEntity LoadRoom(string roomPath, IStateManager stateManager);
    }
}