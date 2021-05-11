using Sat.Recruitment.Api.Helper.Interfaces;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Models.Enum;
using System;
using System.Collections.Generic;
using System.IO;

namespace Sat.Recruitment.Api.Helper
{
    public class Reader: IReader
    {
        public List<User> GetUsers(User user)
        {
            List<User> userList = new List<User>();

            var reader = ReadUsersFromFile();

            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLineAsync().Result.Split(',');

                var userToAdd = new User
                {
                    Name = line[0].ToString(),
                    Email = line[1].ToString(),
                    Phone = line[2].ToString(),
                    Address = line[3].ToString(),
                    UserType = (UserType)Enum.Parse(typeof(UserType), line[4].ToString()),
                    Money = decimal.Parse(line[5].ToString()),
                };
                userList.Add(userToAdd);
            }
            reader.Close();

            return userList;
        }

        private StreamReader ReadUsersFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }
    }
}
