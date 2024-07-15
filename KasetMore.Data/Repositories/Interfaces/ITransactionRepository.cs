using KasetMore.Data.Models;

namespace KasetMore.Data.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task<List<int>> AddTransactions(List<Transaction> transactions);
        Task<int> AddTransaction(Transaction transactions);
        Task<List<Transaction>> GetSellerTransactionsByEmail(string email);
        Task<Transaction> GetSellerTransactionsById(int id);
        Task<List<Transaction>> GetBuyerTransactionsByEmail(string email);
        Task<Transaction?> GetTransactionById(int id);
    }
}