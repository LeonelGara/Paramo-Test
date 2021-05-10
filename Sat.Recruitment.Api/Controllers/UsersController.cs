using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Helper.Interfaces;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Models.Enum;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly IReader _reader;

        public UsersController(IReader reader)
        {
            _reader = reader;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser([FromBody] User user)
        {
            try
            {
                user.ValidateUser();

                user.NormalizeEmail();

                List<User> userList = _reader.GetUsers(user);

                user.ValidateIfUserIsDuplicated(userList);

                user.CalculateMoneyByUserType();

                return new Result()
                {
                    IsSuccess = true,
                    Message = "User Created"
                };
            }
            catch (Exception ex)
            {
                return new Result()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }  
        }
    }
}
