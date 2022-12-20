using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using MT.ReportService.API.Dtos;
using MT.ReportService.Core.Entity;
using MT.ReportService.Core.Event;
using MT.ReportService.Core.Interfaces.Services;
using MT.ReportService.Core.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MT.ReportService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {

        private readonly IGenericService<Report> _reportService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public ReportController(IGenericService<Report> reportService, IMapper mapper, IUnitOfWork unitOfWork)
        {

            _reportService = reportService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
          
        }
        [HttpGet]
        public async Task<IActionResult> GetAllReports()
        {
            var reports = await _reportService.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<ReportDto>>(reports));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateReport([FromBody] ReportDto reportModel)
        {
            await _reportService.AddAsync(_mapper.Map<Report>(reportModel));
            _unitOfWork.AddEvent(new ReportCreatedEvent()
            {
                ReportName = $"{reportModel.ReportName}"
            });
            await _unitOfWork.CompleteAsync();

            return Ok("Success");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var report =await _reportService.GetByIdAsync(id);
            _reportService.Delete(report);
            

            return NoContent();
        }

    }
}
