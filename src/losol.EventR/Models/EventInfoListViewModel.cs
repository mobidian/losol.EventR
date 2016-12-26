using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace losol.EventR.Models
{
    public class EventInfoListViewModel
    {
        public IEnumerable<EventInfo> EventInfos { get; set; }
        public string CurrentCategory { get; set; }
    }
}

