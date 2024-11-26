using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.Users
{
    public class UserListAndCount
    {
        public List<User>? Users { get; set; }
        public int Count { get; set; }
    }
}
