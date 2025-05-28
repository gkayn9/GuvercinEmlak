using Guvercin.Application.Interfaces;
using Guvercin.Domain.Entities;
using Guvercin.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace Guvercin.Persistance.Repository;

public class AdvertItemRepository:IAdvertItemRepository
{
    private readonly AppDbContext _context;

    public AdvertItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<AdvertItem>> GetAdvertItemFilterByCategoryId(int categoryId)
    {
        var result= await _context.AdvertItems.Where(x=>x.CategoryId== categoryId).ToListAsync();
        return result;
    }
}