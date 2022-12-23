using MetricsAgent.Models;
using AutoMapper;
using MetricsAgent.Models.Dto;
using MetricsAgent.Models.Types;

namespace MetricsAgent.Mappings
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            #region CpuMetric
            CreateMap<ValueTime, CpuMetric>()
                .ForMember(x => x.Time,
                opt => opt.MapFrom(src => (long)src.Time.TotalSeconds));

            CreateMap<CpuMetric, CpuMetricDto>();
            #endregion

            #region DotnetMetric
            CreateMap<ValueTime, DotnetMetric>()
            .ForMember(x => x.Time,
            opt => opt.MapFrom(src => (long)src.Time.TotalSeconds));

            CreateMap<DotnetMetric, DotnetMetricDto>();
            #endregion

            #region HddMetric
            CreateMap<ValueTime, HddMetricDto>()
           .ForMember(x => x.Time,
           opt => opt.MapFrom(src => (long)src.Time.TotalSeconds));

            CreateMap<HddMetric, HddMetricDto>();
            #endregion

            #region NetworkMetric
            CreateMap<ValueTime, NetworkMetricDto>()
           .ForMember(x => x.Time,
           opt => opt.MapFrom(src => (long)src.Time.TotalSeconds));

            CreateMap<NetworkMetric, NetworkMetricDto>();
            #endregion

            #region RamMetric
            CreateMap<ValueTime, RamMetricDto>()
           .ForMember(x => x.Time,
           opt => opt.MapFrom(src => (long)src.Time.TotalSeconds));

            CreateMap<RamMetric, RamMetricDto>();
            #endregion
        }
    }
}
