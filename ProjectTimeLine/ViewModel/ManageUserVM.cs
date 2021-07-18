using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTimeLine.ViewModel
{
    public class ManageUserVM
    {
        public ManageUserVM()
        {
        }

        public string NIK { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }
        public string Password { get; set; }
        public ICollection<string> RoleName { get; set; }

        public ManageUserVM(string nIK, string name, string email, string phoneNumber, DateTime birthDate, string address, Gender gender, string password)
        {
            NIK = nIK;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            BirthDate = birthDate;
            Address = address;
            Gender = gender;
            Password = password;
        }
    }


}
