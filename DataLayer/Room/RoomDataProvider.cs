using System;
using System.Collections.Generic;
using DataLayer.Core;
using DataLayer.Data;
using DataLayer.Logic;
using DataLayer.Schema;

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

        public void CreateIfNeeded(string destination)
        {
            if (!_objectProvider.DoesExist(destination))
            {
                var emptyRoom = new RoomSchema
                {
                    Decisions = new List<DecisionSchema>(),
                    Description = "Enter room description",
                    Name = "EnterName"
                };

                _objectProvider.WriteRoom(destination, emptyRoom);
            }
        }

        public RoomSchema LoadRoomSchema(string destination)
        {
            return _objectProvider.ReadRoom(destination);
        }
    }
}