using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources.Models
{
    public class User(int id, string name, string email, bool isAdmin)
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string Email { get; set; } = email;
        public bool IsAdmin { get; set; } = isAdmin;
    }
}
