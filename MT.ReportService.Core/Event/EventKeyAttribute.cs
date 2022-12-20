using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.ReportService.Core.Event
{
    public class EventKeyAttribute : Attribute
    {
        public EventKeyAttribute(string key)
        {
            Key = key;
        }

        public string Key { get; }
    }
}
