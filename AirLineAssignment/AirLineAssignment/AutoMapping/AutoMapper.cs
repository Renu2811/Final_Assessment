using AirLineAssignment.Api;
using AirLineAssignment.Entities;
using AutoMapper;

namespace AirLineAssignment.AutoMapping
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<AirLine, ModelApiAirLine>().ReverseMap();
        }

    }
}
