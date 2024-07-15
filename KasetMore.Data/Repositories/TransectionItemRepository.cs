using KasetMore.ApplicationCore.Models;
using KasetMore.Data.Models;
using KasetMore.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KasetMore.Data.Repositories
{
    public class TransectionItemRepository : ITransectionItemRepository
    {
        private readonly KasetMoreContext _context;
        public TransectionItemRepository(KasetMoreContext context)
        {
            _context = context;
        }
        public async Task<List<int>> AddTransactionItem(List<TransectionItemInsert> transactions)
        {
            try
            {
                var payload = transactions.Select(x => new Models.TransectionItems
                {
                    Amount = x.Amount,
                    Price = x.Price,
                    ProductId = x.ProductId,
                    TransactionId = x.TransectionId,
                    Unit = x.Unit,
                    SellerEmail = x.SellerEmail
                });
                _context.TransactionItems.AddRange(payload);
                foreach (var item in payload.Select(x => new Tuple<int, int>(x.ProductId, x.Amount)))
                {
                    await RemoveProductAmount(item.Item1, item.Item2);
                }
                await _context.SaveChangesAsync();
                return payload.Select(x => x.TransactionItemId).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<TransectionItems>> getTransactionItemById(int transectionId)
        {
            return await _context.TransactionItems.Where(x => x.TransactionId == transectionId).ToListAsync();
        }
        public async Task<List<TransectionItems>> getTransactionItemByEmail(string email)
        {
            return await _context.TransactionItems.Where(x => x.SellerEmail == email).ToListAsync();
        }

        private async Task RemoveProductAmount(int productId, int amount)
        {
            try
            {
                await _context.Products
                    .Where(p => p.ProductId == productId)
                    .ExecuteUpdateAsync(p => p.SetProperty(p => p.Amount, p => p.Amount - amount == 0 ? 0 : p.Amount - amount));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
