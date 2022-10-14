using AutoMapper;
using MetricsAgent.Models;
using MetricsAgent.Models.Dto;
using MetricsAgent.Models.Requests;

namespace MetricsAgent.Mappings
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetricCreateRequest, CpuMetric>()
                .ForMember(x => x.Time,
                opt => opt.MapFrom(src => (long)src.Time.TotalSeconds));

            CreateMap<CpuMetric, CpuMetricDto>();

        }

    }
}