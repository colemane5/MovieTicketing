using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources.Models
{
    public interface IPerson
    {
        int Id { get; set; }
        string Name { get; set; }
        DateTime DoB { get; set; }
    }
}
