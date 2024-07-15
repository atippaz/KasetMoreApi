using KasetMore.ApplicationCore.Models;
using KasetMore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KasetMore.Data.Repositories.Interfaces
{
    public interface ITransectionItemRepository
    {
        Task<List<int>> AddTransactionItem(List<TransectionItemInsert> transactions);
        Task<List<TransectionItems>> getTransactionItemById(int transectionId);
        Task<List<TransectionItems>> getTransactionItemByEmail(string email);

    }
}
