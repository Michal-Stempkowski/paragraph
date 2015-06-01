using System;
using DataLayer.Core;
using DataLayer.Data;
using DataLayer.Logic;

namespace DataLayer.Room
{
    public class RoomDataProvider : IRoomDataProvider
    {
        private readonly IDaoProvider _objectProvider;
        private readonly ICoreTranslator _translator;

        public RoomDataProvider(IDaoProvider objectProvider, ICoreTranslator translator)
        {
            _objectProvider = objectProvider;
            _translator = translator;
        }

        public IEntity LoadRoom(string roomPath, IStateManager stateManager)
        {
            var roomSchema = _objectProvider.ReadRoom(roomPath);

            throw new NotImplementedException();
        }
    }
}