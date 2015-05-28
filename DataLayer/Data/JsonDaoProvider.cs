using System;
using DataLayer.Schema;
using DataLayer.Storage;
using Newtonsoft.Json;

namespace DataLayer.Data
{
    public class JsonDaoProvider : IDaoProvider
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
    }
}
