using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class Admin : User
    {
        public Admin(string username, string pass) : base(username,pass) { }
    }
}
