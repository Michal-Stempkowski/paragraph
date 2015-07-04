using System;
using DataLayer.Exceptions;
using DataLayer.Room;
using DataLayer.Schema;

namespace DataLayer.Logic
{
    public class EntityDataProvider : IEntityDataProvider
    {
        private readonly IRoomDataProvider _roomDataProvider;

        public EntityDataProvider(IRoomDataProvider roomDataProvider)
        {
            _roomDataProvider = roomDataProvider;
        }

        public const string RoomFileExtension = ".room";

        public bool PerformEntityTransition(string entityPath, IStateManager stateManager)
        {
            if (String.IsNullOrWhiteSpace(entityPath) || !entityPath.EndsWith(RoomFileExtension))
            {
                return false;
            }

            try
            {
                CurrentEntity = _roomDataProvider.LoadRoom(entityPath, stateManager);
                return true;
            }
            catch (LoadFailedException)
            {
                    
            }

            return false;
        }

        public IEntity CurrentEntity { get; private set; }
        public void CreateRoomIfIdDoesNotExist(string destination)
        {
            _roomDataProvider.CreateIfNeeded(destination);
        }

        public RoomSchema LoadRawSchema(string destination)
        {
            return _roomDataProvider.LoadRoomSchema(destination);
        }
    }
}