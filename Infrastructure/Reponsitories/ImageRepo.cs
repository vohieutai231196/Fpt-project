using Application.Repositories;
using Domain;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ImageRepo : IImageRepo
    {
        private readonly ApplicationDBContext _context;

        public ImageRepo(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task AddNewAsync(Image image)
        {
            await _context.images.AddAsync(image);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Image image)
        {
            _context.images.Remove(image);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Image>> GetAllAsync()
        {
            return await _context.images
                .Include(i => i.Property)
                .ToListAsync();
        }

        public async Task<Image> GetByIdAsync(int id)
        {
            return await _context.images
                .Include(i => i.Property)
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Image image)
        {
            _context.images.Update(image);
            await _context.SaveChangesAsync();
        }
    }
}
