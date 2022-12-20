using AutoMapper;
using MT.ReportService.API.Dtos;
using MT.ReportService.Core.Entity;

namespace MT.ReportService.API.Mapping
{
    public class MapProfile : Profile
    {

        public MapProfile()
        {
            CreateMap<Report, ReportDto>().ReverseMap();
      
        }
    }
}
