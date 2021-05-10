using Sat.Recruitment.Api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Helper.Interfaces
{
    public interface IReader
    {
        List<User> GetUsers(User user);
    }
}
