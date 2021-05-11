using Sat.Recruitment.Api.Models;
using System.Collections.Generic;

namespace Sat.Recruitment.Api.Helper.Interfaces
{
    public interface IReader
    {
        List<User> GetUsers(User user);
    }
}
