using System;
using DataLayer.Logic;
using DataLayer.Schema;
using DataLayer.Storage;
using Newtonsoft.Json;

namespace DataLayer.Data
{
    public class JsonDaoProvider<TStateManager> : IDaoProvider
        where TStateManager : IStateManager
    {
        private readonly IStorageSupervisor _storageSupervisor;

        public JsonDaoProvider(IStorageSupervisor storageSupervisor)
        {
            _storageSupervisor = storageSupervisor;
        }

        public RoomSchema ReadRoom(string pathToRoom)
        {
            string serializedRoom = _storageSupervisor.Read(pathToRoom);
            return JsonConvert.DeserializeObject<RoomSchema>(serializedRoom);
        }

        public void WriteRoom(string path, RoomSchema room)
        {
            string serializedRoom = JsonConvert.SerializeObject(room);
            _storageSupervisor.Write(path, serializedRoom);
        }

        public void WriteStateManager(string path, IStateManager stateManager)
        {
            string serializedManager = JsonConvert.SerializeObject(stateManager);
            _storageSupervisor.Write(path, serializedManager);
        }

        public IStateManager ReadStateManager(string path)
        {
            string serializedManager = _storageSupervisor.Read(path);
            return JsonConvert.DeserializeObject<TStateManager>(serializedManager);
        }
    }
}
