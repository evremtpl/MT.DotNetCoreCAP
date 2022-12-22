using DotNetCore.CAP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MT.ReportService.Core.Event;
using System;
using System.Threading.Tasks;

namespace MT.ConsumerService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumerController : ControllerBase
    {
        [CapSubscribe("reportcreatedevent")]
        public async Task<IActionResult> Index(ReportCreatedEvent message)
        {
            Console.WriteLine(message);

            return Ok();
        }
        //public void Consumer(ReportCreatedEvent message)
        //{
        //    Console.WriteLine(message);
        //}
    }
}
