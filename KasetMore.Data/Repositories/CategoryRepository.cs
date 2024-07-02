using KasetMore.Data.Models;
using KasetMore.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KasetMore.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly KasetMoreContext _context;

        public CategoryRepository(KasetMoreContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
