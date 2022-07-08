using AutoMapper;

namespace faktura.Services.Mappers
{
    public class Mapper:Profile
    {
        public Mapper()
        {
            CreateMap<Data.Models.Faktura, Data.Dtos.Responses.Faktura>();
            CreateMap<Data.Dtos.Requests.Faktura, Data.Models.Faktura>().ReverseMap();

            CreateMap<Data.Models.StavkeFakture, Data.Dtos.Requests.StavkeFakture>().ReverseMap();
            CreateMap<Data.Models.StavkeFakture, Data.Dtos.Responses.StavkeFakture>();
        }
    }
}
