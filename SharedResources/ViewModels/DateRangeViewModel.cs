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

        public DateRangeViewModel()
        {
            From = new DateTime(2024, 1, 1);
            To = DateTime.Now;
        }
    }
}
