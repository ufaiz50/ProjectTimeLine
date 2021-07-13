using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTimeLine.ViewModel
{
    public class DataLoginVM
    {
        public DataLoginVM()
        {
        }

        public DataLoginVM(string name, string email, string role)
        {
            Name = name;
            Email = email;
            Role = role;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }


    }


}
