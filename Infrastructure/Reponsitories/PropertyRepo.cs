using Application.Responsitories;
using Domain;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reponsitories
{
    public class PropertyRepo : IPropertyRepon
    {
        private readonly ApplicationDBContext _context;
        public PropertyRepo(ApplicationDBContext context)
        {
               _context = context;
        }
        public async Task AddNewAsync(Property property)
        {
            await _context.Properties.AddAsync(property);
            await _context.SaveChangesAsync();
        }

        public Task DeleteAsync(Property property)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Property>> GetAllAsync()
        {
            return await _context.Properties.ToListAsync();
        }

        public async Task<Property> GetByIdAsync(int id)
        {
            return await _context.Properties.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Property property)
        {
            _context.Properties.Update(property);
            await _context.SaveChangesAsync();
        }
    }
}
