using SharedResources.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources.ViewModels
{
    public abstract class DateRangeViewModel : RefreshableViewModel
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
