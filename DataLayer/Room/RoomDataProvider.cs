using DataLayer.Logic;
using Tests;

namespace DataLayer.Room
{
    public class RoomDataProvider : IRoomDataProvider
    {
        private readonly IJsonObjectProvider _objectProvider;

        public RoomDataProvider(IJsonObjectProvider objectProvider)
        {
            _objectProvider = objectProvider;
        }

        public IEntity LoadRoom(string roomPath, IStateManager stateManager)
        {
            return _objectProvider.ReadRoom(roomPath);
        }
    }
}