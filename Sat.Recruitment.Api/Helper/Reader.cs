using Sat.Recruitment.Api.Helper.Interfaces;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Models.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
                var line = reader.ReadLineAsync().Result;
                var userToAdd = new User
                {
                    Name = line.Split(',')[0].ToString(),
                    Email = line.Split(',')[1].ToString(),
                    Phone = line.Split(',')[2].ToString(),
                    Address = line.Split(',')[3].ToString(),
                    UserType = (UserType)Enum.Parse(typeof(UserType), line.Split(',')[4].ToString()),
                    Money = decimal.Parse(line.Split(',')[5].ToString()),
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
