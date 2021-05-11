using Sat.Recruitment.Api.Models.Enum;
using System;
using System.Collections.Generic;

namespace Sat.Recruitment.Api.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public UserType UserType { get; set; }
        public decimal Money { get; set; }

        public User()
        {

        }

        public void ValidateUser()
        {
            var errors = "";
            if (String.IsNullOrEmpty(Name))
                errors = "The name is required";
            if (String.IsNullOrEmpty(Email))
                errors = errors + " The email is required";
            if (String.IsNullOrEmpty(Address))
                errors = errors + " The address is required";
            if (String.IsNullOrEmpty(Phone))
                errors = errors + " The phone is required";

            if (!String.IsNullOrEmpty(errors))
                throw new Exception(errors);
        }

        public void CalculateMoneyByUserType()
        {
            decimal percentage = 0;
            decimal gif = 0;

            if (Money > 100)
                switch (UserType)
                {
                    case UserType.Normal:
                        percentage = Convert.ToDecimal(0.12);
                        break;
                    case UserType.Premium:
                        gif = Money * 2;
                        break;
                    case UserType.SuperUser:
                        percentage = Convert.ToDecimal(0.20);
                        break;
                    default:
                        break;
                }
            else if (Money > 10 && UserType.Normal == UserType)
                percentage = Convert.ToDecimal(0.8);

            if (percentage != 0)
                gif = Money * percentage;

            if (gif != 0)
                Money = Money + gif;
        }

        public void ValidateIfUserIsDuplicated(List<User> userList)
        {
            foreach (var userToCompare in userList)
            {
                if ((userToCompare.Email == Email || userToCompare.Phone == Phone) || (userToCompare.Name == Name && userToCompare.Address == Address))
                    throw new Exception("The user is duplicated");
            }
        }

        public void NormalizeEmail()
        {
            var aux = Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            Email = string.Join("@", new string[] { aux[0], aux[1] });
        }
    }
}
