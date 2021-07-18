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
            AllRole = new List<string>();
        }

        public DataLoginVM(string nIK,string name, string email, string role)
        {
            NIK = nIK;
            Name = name;
            Email = email;
            Role = role;
        }

        public string NIK { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public IList<string> AllRole { get; set; }
        

    }


}
