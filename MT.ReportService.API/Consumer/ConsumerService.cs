using DotNetCore.CAP;
using MT.ReportService.Core.Event;
using System;

namespace MT.ReportService.API.Consumer
{
    public class ConsumerService : ICapSubscribe
    {
        [CapSubscribe("reportcreatedevent")]
        public void Consumer(ReportCreatedEvent value)
        {
            Console.WriteLine(value);
            //throw new InvalidOperationException(); -- To view faulty events on the dashboard

        }
    }
}
