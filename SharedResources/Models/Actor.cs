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
        public DateTime DoB { get; set; }

        public Actor(string name, DateTime date) 
        { 
            Name = name;
            DoB = date;
        }
    }
}
