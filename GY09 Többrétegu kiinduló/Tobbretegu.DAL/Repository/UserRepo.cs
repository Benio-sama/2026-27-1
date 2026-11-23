using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Tobbretegu.Model;

namespace Tobbretegu.Repository
{
    public class UserRepo
    {
        private static readonly string path = Path.Combine(Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"Tobbretegu")).FullName,"users.xml");
        private static readonly object _syncLock = new object();
        public static UserList LoadUsersFromFile()
        {
            string path = Path.Combine(
                Directory.CreateDirectory(Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Tobbretegu")).FullName,
                "users.xml");
            lock (_syncLock)
            {
                // read the file content
                if (File.Exists(path))
                {
                    try
                    {
                        var serializer = new XmlSerializer(typeof(UserList));
                        using (var fs = File.OpenRead(path))
                            return (UserList)serializer.Deserialize(fs);
                    }
                    catch
                    {
                        // if the file is invalid, fall back to defaults
                    }
                }

                // provides defaults for debugging
                return new UserList()
                { 
                    new User()
                    {
                        Id = Guid.Parse("87923C8F-17FC-4EDF-99D3-68568BFDB93B"),
                        Name = "John",
                    },
                    new User()
                    {
                        Id = Guid.Parse("8CCB7032-7099-4D70-AE50-26EEC9F8EE8E"),
                        Name = "Mary",
                    }
                };
            }
        }

        public static void SaveUsersToFile(UserList allUsers)
        {
            string path = Path.Combine(
                Directory.CreateDirectory(Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Tobbretegu")).FullName,
                "users.xml");
            lock (_syncLock)
            {
                var serializer = new XmlSerializer(typeof(UserList));
                using (var fs = File.Create(path))
                    serializer.Serialize(fs, allUsers);
            }
        }
    }
}
