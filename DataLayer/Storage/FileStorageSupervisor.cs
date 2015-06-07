using System;
using System.IO;
using System.Net.Mime;
using System.Reflection;

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
            using (var file = new StreamWriter(path, append: false))
            {
                file.Write(serializedRoom);
            }
        }

        public string Read(string path)
        {
            using (var file = new StreamReader(path))
            {
                return file.ReadToEnd();
            }
        }
    }
}