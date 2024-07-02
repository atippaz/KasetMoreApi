using KasetMore.Data.Models;

namespace KasetMore.Data.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategories();
    }
}