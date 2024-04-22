using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources.Models
{
    public struct Actor(int id, string name, DateTime date) : IPerson
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public DateTime DoB { get; set; } = date;
    }
}
