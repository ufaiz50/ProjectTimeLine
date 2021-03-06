using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models
{
    public class UserDataVM
    {
        public UserDataVM()
        {
            AllRoleName = new List<string>();
        }

        public string NIK { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }
        public string RoleName { get; set; }

        public IList<string> AllRoleName { get; set; }

        
    }

    public enum Gender
    {
        Pria,
        Wanita
    }
}
