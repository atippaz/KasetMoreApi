using KasetMore.Data.Models;

namespace KasetMore.Data.Repositories.Interfaces
{
    public interface IUnitRepository
    {
        Task<List<Unit>> GetUnits();
    }
}