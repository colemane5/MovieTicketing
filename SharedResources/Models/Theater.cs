using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources.Models
{
    public struct Theater(int id, string name, string address)
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string Address { get; set; } = address;
    }
}
