using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace La_Subasta
{
    public class User
    {
        public string Role { get; }

        public User(string role)
        {
            Role = role;
        }
    }

}
