using System;
using DataLayer.Data;
using DataLayer.Logic;

namespace DataLayer.Room
{
    public class RoomDataProvider : IRoomDataProvider
    {
        private readonly IDaoProvider _objectProvider;

        public RoomDataProvider(IDaoProvider objectProvider)
        {
            _objectProvider = objectProvider;
        }

        public IEntity LoadRoom(string roomPath, IStateManager stateManager)
        {
            var roomSchema = _objectProvider.ReadRoom(roomPath);
            throw new NotImplementedException();
        }
    }
}