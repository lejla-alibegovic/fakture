using AutoMapper;

namespace faktura.Services.Mappers
{
    public interface IFakturaMapper
    {
        IMapper Mapper { get; set; }
    }
    public class FakturaMapper : IFakturaMapper
    {
        public IMapper Mapper { get ; set; }
        public FakturaMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Mapper>();
            });
            Mapper = config.CreateMapper();
        }
    }
}
