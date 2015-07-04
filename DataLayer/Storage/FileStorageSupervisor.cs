using System.IO;

namespace DataLayer.Storage
{
    public class FileStorageSupervisor : IStorageSupervisor
    {
        public readonly string RootPath;

        public FileStorageSupervisor(string rootPath)
        {
            RootPath = rootPath;
        }

        public void Write(string path, string serializedRoom)
        {
            path = AddRootIfApplies(path);
            using (var file = new StreamWriter(path, append: false))
            {
                file.AutoFlush = true;
                file.Write(serializedRoom);
            }
        }

        public string Read(string path)
        {
            path = AddRootIfApplies(path);
            using (var file = new StreamReader(path))
            {
                return file.ReadToEnd();
            }
        }

        private string AddRootIfApplies(string path)
        {
            return path.Contains(":") ? path : RootPath + Path.DirectorySeparatorChar + path;
        }

        public bool DoesExist(string destination)
        {
            return File.Exists(destination);
        }
    }
}