using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Tobbretegu.Model;

namespace Tobbretegu.Logic
{
    public class UserService
    {   
        private UserList allUsers;
        public UserService()
        {
            allUsers = Repository.UserRepo.LoadUsersFromFile();
        }
        public IEnumerable<User> ListAll()
        {
            return allUsers;
        }
        public User Find(Guid? userId)
        {
            foreach (var u in allUsers)
            {
                if (u.Id == userId)
                    return u;
            }
            return null;
        }
        public void Add(User newUser)
        {
            allUsers.Add(newUser);
            Repository.UserRepo.SaveUsersToFile(allUsers);
        }
        public void ChangeName(Guid userId, string newUserName)
        {
            foreach (var u in allUsers)
            {
                if (u.Id == userId)
                    u.Name = newUserName;
            }
            Repository.UserRepo.SaveUsersToFile(allUsers);
        }

        public string GetNewUserName()
        {
            string newUserName;
            int counter = 1;
            while (true)
            {
                newUserName = $"User {counter++}";
                if (!usernameExists(newUserName))
                    break;
            }

            return newUserName;
        }

        private bool usernameExists(string newUserName)
        {
            foreach (var u in allUsers)
            {
                if (u.Name == newUserName)
                    return true;
            }
            return false;
        }

    }
}
