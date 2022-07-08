using faktura.Data.Context;
using faktura.Services.IServices;
using faktura.Services.Mappers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.Entity;
using System;

namespace faktura.Services.Services
{
    public class Base<TEntity, TRequest, TResponse, TKey>
        : IBase<TRequest, TResponse, TKey> where TEntity : class
    {
        private readonly Context _context;
        private readonly IFakturaMapper _mapper;

        public Base(Context context, IFakturaMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        public virtual async Task<TResponse> Create(TRequest entity)
        {
            TEntity newEntity = _mapper.Mapper.Map<TEntity>(entity);
            _mapper.Mapper.Map<TResponse>(_context.Set<TEntity>().Add(newEntity));
            await _context.SaveChangesAsync();
            return _mapper.Mapper.Map<TResponse>(newEntity);
        }

        public virtual async Task Delete(TKey id)
        {
            TEntity dbEntity = await _context.Set<TEntity>().FindAsync(id);
            _context.Set<TEntity>().Remove(dbEntity ?? throw new InvalidOperationException("Entity does not exist!"));
            await _context.SaveChangesAsync();
        }

        public virtual async Task<List<TResponse>> GetAll()
        {
            return _mapper.Mapper.Map<List<TResponse>>(await _context.Set<TEntity>().ToListAsync());
        }

        public virtual async Task<TResponse> GetById(TKey id)
        {
            return _mapper.Mapper.Map<TResponse>(await _context.Set<TEntity>().FindAsync(id));
        }

        public virtual async  Task<TRequest> GetRequestTypeById(TKey id)
        {
            return _mapper.Mapper.Map<TRequest>(await _context.Set<TEntity>().FindAsync(id));
        }

        public virtual async Task<TResponse> Update(TRequest entity, TKey id)
        {
            TEntity dbEntity = await _context.Set<TEntity>().FindAsync(id);
            _context.Set<TEntity>().Attach(dbEntity ?? throw new InvalidOperationException("Entity does not exist!"));
            _mapper.Mapper.Map(entity, dbEntity);
            await _context.SaveChangesAsync();
            return _mapper.Mapper.Map<TResponse>(dbEntity);
        }
    }
}
