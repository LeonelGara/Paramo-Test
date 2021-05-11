using System;
using System.Collections.Generic;
using System.Dynamic;

using Microsoft.AspNetCore.Mvc;
using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Helper.Interfaces;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Models.Enum;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1
    {
        [Fact]
        public void UserCreatedSuccess()
        {
            var IReaderMock = new Mock<IReader>();

            var user = new User
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Phone = "+349 1122354215",
                Address = "Av. Juan G",
                UserType = UserType.Normal,
                Money = 1234,
            };

            var userList = new List<User>() {
                new User {
                        Name = "Lara",
                        Email = "lara@gmail.com",
                        Phone = "+9 1122354215",
                        Address = "Av. G",
                        UserType = UserType.Premium,
                        Money = 123,
                    },
                 new User {
                        Name = "Juan",
                        Email = "Marcoss@gmail.com",
                        Phone = "+349 12354215",
                        Address = "Av. moreno",
                        UserType = UserType.Normal,
                        Money = 1234,
                    }
            };

            IReaderMock.Setup(c => c.GetUsers(user)).Returns(userList).Verifiable();

            var userController = new UsersController(IReaderMock.Object);

            var result = userController.CreateUser(user).Result;

            user.NormalizeEmail();
            user.CalculateMoneyByUserType();

            IReaderMock.Verify();
            Assert.True(result.IsSuccess);
            Assert.Equal("User Created", result.Message);
        }

        [Fact]
        public void UserDuplicatedError()
        {
            var IReaderMock = new Mock<IReader>();

            var user = new User
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Phone = "+349 1122354215",
                Address = "Av. Juan G",
                UserType = UserType.Normal,
                Money = 1234,
            };

            var userList = new List<User>() {
                new User {
                        Name = "Lara",
                        Email = "lara@gmail.com",
                        Phone = "+9 1122354215",
                        Address = "Av. G",
                        UserType = UserType.Premium,
                        Money = 123,
                    },
                 new User {
                        Name = "Mike",
                        Email = "mike@gmail.com",
                        Phone = "+349 1122354215",
                        Address = "Av. Juan G",
                        UserType = UserType.Normal,
                        Money = 1234,
                    }
            };

            IReaderMock.Setup(c => c.GetUsers(user)).Returns(userList).Verifiable();

            var userController = new UsersController(IReaderMock.Object);

            var result = userController.CreateUser(user).Result;

            user.NormalizeEmail();
            IReaderMock.Verify();
            Assert.False(result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Message);
            Assert.Throws<Exception>(() => user.ValidateIfUserIsDuplicated(userList));
        }

        [Fact]
        public void UserValidationBodyError()
        {
            var IReaderMock = new Mock<IReader>();

            var user = new User
            {
                Name = "",
                Email = "",
                Phone = "",
                Address = "",
                UserType = UserType.Normal,
                Money = 1234,
            };

            var userList = new List<User>() {
                new User {
                        Name = "Lara",
                        Email = "lara@gmail.com",
                        Phone = "+9 1122354215",
                        Address = "Av. G",
                        UserType = UserType.Premium,
                        Money = 123,
                    },
                 new User {
                        Name = "Oiki",
                        Email = "marco@gmail.com",
                        Phone = "+349 1122354215",
                        Address = "Av. Marcoss",
                        UserType = UserType.Normal,
                        Money = 1234,
                    }
            };

            IReaderMock.Setup(c => c.GetUsers(user)).Returns(userList).Verifiable();

            var userController = new UsersController(IReaderMock.Object);

            var result = userController.CreateUser(user).Result;

            Assert.False(result.IsSuccess);
            Assert.Equal("The name is required The email is required The address is required The phone is required", result.Message);
            Assert.Throws<Exception>(() => user.ValidateUser());
        }
    }
}
