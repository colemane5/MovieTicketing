using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources.Models
{
    public class Actor
    {
        public string Name { get; set; }
        public DateOnly DoB { get; set; }

        public Actor(string name, DateOnly date) 
        { 
            Name = name;
            DoB = date;
        }
    }
}
