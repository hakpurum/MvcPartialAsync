using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcPartialAsync.Models
{
    public class UserModel
    {
        public UserModel()
        {
            Users = new List<User>();
        }
        public List<User> Users { get; set; }
    }
    public class User
    {
        public string UserName { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public int UserId { get; set; }
    }
}
