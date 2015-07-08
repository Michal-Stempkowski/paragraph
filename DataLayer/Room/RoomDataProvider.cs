using System;
using System.Collections.Generic;
using System.Linq;
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
            var entity = new Entity
            {
                Description = roomSchema.Description,
                Decisions = roomSchema.Decisions.Select(x => new Decision
                {
                    Description = x.Description,
                    Destination = x.Destination,
                    Effect = (manager) => _translator.ExpandToBool(x.Effect, manager),
                    IsVisible = _translator.ExpandToBool(x.VisibilityRequirements, stateManager)
                } as IDecision).ToList(),
            };

            return entity;
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
            return _objectProvider.ReadRoom(destination) ?? new RoomSchema()
            {
                Decisions = new List<DecisionSchema>(),
                Description = "___Invalid_room_file__",
                Name = "Invalid room"
            };
        }

        public void SaveRoomSchema(string destination, RoomSchema roomSchema)
        {
            _objectProvider.WriteRoom(destination, roomSchema);
        }

        public IStateManager LoadStateManager(string destination)
        {
            return _objectProvider.ReadStateManager(destination);
        }

        public void SaveStateManager(string destination, IStateManager stateManager)
        {
            _objectProvider.WriteStateManager(destination, stateManager);
        }
    }
}