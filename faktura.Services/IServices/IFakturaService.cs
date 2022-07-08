using faktura.PDV;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace faktura.Services.IServices
{
    public interface IFakturaService:IBase<Data.Dtos.Requests.Faktura, Data.Dtos.Responses.Faktura, int>
    {
        Task<List<Data.Dtos.Responses.Faktura>> GetAllByUserId(string userId);
        Task<Data.Dtos.Responses.Faktura> CreateWithPDV(Data.Dtos.Requests.Faktura entity, IPDV taxService);
        Task<Data.Dtos.Responses.Faktura> UpdateWithPDV(int id, Data.Dtos.Requests.Faktura entity, IPDV taxService);

    }
}
