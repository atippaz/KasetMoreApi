using KasetMore.Data.Models;
using KasetMore.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KasetMore.Data.Repositories
{
    public class UnitRepository : IUnitRepository
    {
        private readonly KasetMoreContext _context;

        public UnitRepository(KasetMoreContext context)
        {
            _context = context;
        }

        public async Task<List<Unit>> GetUnits()
        {
            return await _context.Units.ToListAsync();
        }
    }
}
