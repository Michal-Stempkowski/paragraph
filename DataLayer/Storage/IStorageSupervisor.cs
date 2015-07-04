using DataLayer.Schema;

namespace DataLayer.Storage
{
    public interface IStorageSupervisor
    {
        void Write(string path, string serializedRoom);
        string Read(string path);
        bool DoesExist(string destination);
    }
}