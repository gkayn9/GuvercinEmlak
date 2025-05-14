using Guvercin.Application.Interfaces;
using Guvercin.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace Guvercin.Persistance.Repository;

public class GenericRepository<T>:IGenericRepository<T> where T : class
{
    private readonly AppDbContext _context;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var result = await _context.Set<T>().FindAsync(id);
        return result;
    }

    public Task<List<T>> GetAllAsync()
    {
        var result = _context.Set<T>().ToListAsync();
        return result;
    }

    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
         _context.Set<T>().Remove(entity);
         await _context.SaveChangesAsync();
    }
}

