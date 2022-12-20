

namespace MT.ReportService.Core.Event
{
    [EventKey("reportcreatedevent")]
    public class ReportCreatedEvent :EventBase
    {
        public string ReportName { get; set; }
    }
}
