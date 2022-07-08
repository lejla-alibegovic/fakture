using AutoMapper.QueryableExtensions;
using faktura.Data.Context;
using faktura.PDV;
using faktura.Services.IServices;
using faktura.Services.Mappers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace faktura.Services.Services
{
    public class FakturaService : Base<Data.Models.Faktura, Data.Dtos.Requests.Faktura, Data.Dtos.Responses.Faktura, int>,
        IFakturaService
    {
        private readonly Context _context;
        private readonly IFakturaMapper _mapper;

        public FakturaService(Context context, IFakturaMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Data.Dtos.Responses.Faktura> CreateWithPDV(Data.Dtos.Requests.Faktura entity, IPDV pdvService)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var fakturaModel = _mapper.Mapper.Map<Data.Models.Faktura>(entity);
                    fakturaModel.UkupnaCijenaBezPDV = fakturaModel.StavkeFakture.Sum(x => x.UkupnaCijenaBezPDV);
                    fakturaModel.UkupnaCijenaSaPDV = pdvService.Izracunaj(fakturaModel.UkupnaCijenaBezPDV) + fakturaModel.UkupnaCijenaBezPDV;
                    _context.Fakture.Add(fakturaModel);
                    await _context.SaveChangesAsync();
                    transaction.Commit();

                    return _mapper.Mapper.Map<Data.Dtos.Responses.Faktura>(fakturaModel);
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public Task<List<Data.Dtos.Responses.Faktura>> GetAllByUserId(string userId)
        {
            return _context.Fakture.Where(x => x.ApplicationUserId == userId).ProjectTo<Data.Dtos.Responses.Faktura>(_mapper.Mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<Data.Dtos.Responses.Faktura> UpdateWithPDV(int id, Data.Dtos.Requests.Faktura entity, IPDV pdvService)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var dbEntity = await _context.Fakture.FirstOrDefaultAsync(x => x.Id == id);
                    dbEntity.DatumKreiranja = entity.DatumKreiranja;
                    dbEntity.DatumDospijeca = entity.DatumDospijeca;
                    dbEntity.BrojFakture = entity.BrojFakture;
                    dbEntity.PrimateljFakture = entity.PrimateljFakture;
                    dbEntity.UkupnaCijenaBezPDV = entity.StavkeFakture.Sum(x => x.UkupnaCijenaBezPDV);
                    dbEntity.UkupnaCijenaSaPDV = pdvService.Izracunaj(dbEntity.UkupnaCijenaBezPDV) + dbEntity.UkupnaCijenaBezPDV;
                    await _context.SaveChangesAsync().ConfigureAwait(false);


                    var fakturaStavke = await _context.StavkeFakture.Where(x => x.FakturaId == dbEntity.Id).AsNoTracking().ToListAsync();
                    var fakturaStavkeToAdd = entity.StavkeFakture.Where(x => !x.Id.HasValue).Select(x => new Data.Models.StavkeFakture()
                    {
                        Opis = x.Opis,
                        FakturaId = dbEntity.Id,
                        Kolicina = x.Kolicina,
                        UkupnaCijenaBezPDV = x.UkupnaCijenaBezPDV,
                        JedininaCijena = x.JedininaCijena
                    }).ToList();
                    if (fakturaStavkeToAdd.Any())
                    {
                        _context.StavkeFakture.AddRange(fakturaStavkeToAdd);
                    }

                    var fakturaStavkeToEdit = entity.StavkeFakture.Where(x => fakturaStavke.Any(y => y.Id == x.Id) && x.Id.HasValue).ToList();
                    if (fakturaStavkeToEdit.Any())
                    {
                        fakturaStavkeToEdit.ForEach(invoiceItem =>
                        {
                            var item = new Data.Models.StavkeFakture { Id = invoiceItem.Id.Value };
                            _context.StavkeFakture.Attach(item);
                            item.Opis = invoiceItem.Opis;
                            item.JedininaCijena = invoiceItem.JedininaCijena;
                            item.UkupnaCijenaBezPDV = invoiceItem.UkupnaCijenaBezPDV;
                            item.Kolicina = invoiceItem.Kolicina;
                        });
                    }

                    var fakturaStavkeToDelete = fakturaStavke.Where(x => entity.StavkeFakture.All(y => y.Id != x.Id)).ToList();
                    if (fakturaStavkeToDelete.Any())
                    {
                        fakturaStavkeToDelete.ForEach(invoiceItem =>
                        {
                            _context.StavkeFakture.Attach(invoiceItem);
                            _context.StavkeFakture.Remove(invoiceItem);
                        });
                    }

                    await _context.SaveChangesAsync().ConfigureAwait(false);
                    transaction.Commit();

                    return _mapper.Mapper.Map<Data.Dtos.Responses.Faktura>(dbEntity);
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public override async Task<Data.Dtos.Responses.Faktura> Create(Data.Dtos.Requests.Faktura entity)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var fakturaModel = _mapper.Mapper.Map<Data.Models.Faktura>(entity);
                    _context.Fakture.Add(fakturaModel);
                    await _context.SaveChangesAsync();
                    transaction.Commit();

                    return _mapper.Mapper.Map<Data.Dtos.Responses.Faktura>(fakturaModel);
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public override async Task<Data.Dtos.Responses.Faktura> Update(Data.Dtos.Requests.Faktura entity, int id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var dbEntity = await _context.Fakture.FirstOrDefaultAsync(x => x.Id == id);
                    dbEntity.DatumKreiranja = entity.DatumKreiranja;
                    dbEntity.DatumDospijeca = entity.DatumDospijeca;
                    dbEntity.BrojFakture = entity.BrojFakture;
                    dbEntity.PrimateljFakture = entity.PrimateljFakture;
                    await _context.SaveChangesAsync().ConfigureAwait(false);


                    var stavkeFakture = await _context.StavkeFakture.Where(x => x.FakturaId == dbEntity.Id).AsNoTracking().ToListAsync();
                    var stavkeFaktureToAdd = entity.StavkeFakture.Where(x => !x.Id.HasValue).Select(x => new Data.Models.StavkeFakture()
                    {
                        Opis = x.Opis,
                        FakturaId = dbEntity.Id,
                        Kolicina = x.Kolicina,
                        UkupnaCijenaBezPDV = x.UkupnaCijenaBezPDV,
                        JedininaCijena = x.JedininaCijena
                    }).ToList();
                    if (stavkeFaktureToAdd.Any())
                    {
                        _context.StavkeFakture.AddRange(stavkeFaktureToAdd);
                    }

                    var stavkeFaktureToEdit = entity.StavkeFakture.Where(x => stavkeFakture.Any(y => y.Id == x.Id) && x.Id.HasValue).ToList();
                    if (stavkeFaktureToEdit.Any())
                    {
                        stavkeFaktureToEdit.ForEach(invoiceItem =>
                        {
                            var item = new Data.Models.StavkeFakture { Id = invoiceItem.Id.Value };
                            _context.StavkeFakture.Attach(item);
                            item.Opis = invoiceItem.Opis;
                            item.JedininaCijena = invoiceItem.JedininaCijena;
                            item.UkupnaCijenaBezPDV = invoiceItem.UkupnaCijenaBezPDV;
                            item.Kolicina = invoiceItem.Kolicina;
                        });
                    }

                    var stavkeFaktureToDelete = stavkeFakture.Where(x => entity.StavkeFakture.All(y => y.Id != x.Id)).ToList();
                    if (stavkeFaktureToDelete.Any())
                    {
                        stavkeFaktureToDelete.ForEach(invoiceItem =>
                        {
                            _context.StavkeFakture.Attach(invoiceItem);
                            _context.StavkeFakture.Remove(invoiceItem);
                        });
                    }

                    await _context.SaveChangesAsync().ConfigureAwait(false);
                    transaction.Commit();

                    return _mapper.Mapper.Map<Data.Dtos.Responses.Faktura>(dbEntity);
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public override async Task<Data.Dtos.Responses.Faktura> GetById(int id)
        {
            return _mapper.Mapper.Map<Data.Dtos.Responses.Faktura>(await _context.Fakture.Include("StavkeFakture")
                .FirstOrDefaultAsync(x => x.Id == id));
        }

        public override async Task<Data.Dtos.Requests.Faktura> GetRequestTypeById(int id)
        {
            var faktura = await _context.Fakture.Include("StavkeFakture")
                .FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Mapper.Map<Data.Dtos.Requests.Faktura>(faktura);
        }

    }
}
