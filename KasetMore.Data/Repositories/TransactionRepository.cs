using KasetMore.Data.Models;
using KasetMore.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KasetMore.Data.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly KasetMoreContext _context;

        public TransactionRepository(KasetMoreContext context)
        {
            _context = context;
        }

        public async Task<List<Transaction>> GetSellerTransactionsByEmail(string email)
        {
            return await _context.Transactions
                .Where(t => t.SellerEmail == email)
                .ToListAsync();
        }
        public async Task<List<Transaction>> GetBuyerTransactionsByEmail(string email)
        {
            return await _context.Transactions
                .Where(t => t.BuyerEmail == email)
                .ToListAsync();
        }
        public async Task<Transaction?> GetTransactionById(int id)
        {
            return await _context.Transactions
                .Where(t => t.TransactionId == id)
                .FirstOrDefaultAsync();
        }
        public async Task<List<int>> AddTransaction(List<Transaction> transactions)
        {
            try
            {
                _context.Transactions.AddRange(transactions);
                await _context.SaveChangesAsync();
                await RemoveProductAmount(transactions.First());
                return transactions.Select(t => t.TransactionId).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task RemoveProductAmount(Transaction transaction)
        {
            try
            {
                await _context.Products
                    .Where(p => p.ProductId == transaction.ProductId)
                    .ExecuteUpdateAsync(p => p.SetProperty(p => p.Amount, p => p.Amount - transaction.Amount == 0 ? 0 : p.Amount - transaction.Amount));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
