using System;


namespace MT.ReportService.API.Dtos
{
    public enum FileStatus
    {
        Creating,
        Completed
    }
    public class ReportDto
    {
        public int Id { get; set; }
        public string ReportName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
