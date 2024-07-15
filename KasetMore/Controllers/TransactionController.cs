using KasetMore.ApplicationCore.Models;
using KasetMore.Data.Models;
using KasetMore.Data.Repositories;
using KasetMore.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KasetMore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ITransectionItemRepository _transectionItemRepository;

        public TransactionController(ITransactionRepository transactionRepository, ITransectionItemRepository transectionItemRepository)
        {
            _transactionRepository = transactionRepository;
            _transectionItemRepository = transectionItemRepository;
        }

        public class ResultBuyer : Transaction
        {
            public int? TransactionItemId { get; set; }

        }
        [HttpGet("get-by-seller")]
        public async Task<IActionResult> GetTransactionsBySeller(string sellerEmail)
        {
            try
            {
                var result = new List<ResultBuyer>();
                var resultOld = await _transactionRepository.GetSellerTransactionsByEmail(sellerEmail);
                result = resultOld.Select(x => new ResultBuyer
                {
                    Amount = x.Amount,
                    BuyerEmail = x.BuyerEmail,
                    CreateDate = x.CreateDate,
                    Price = x.Price,
                    ProductId = x.ProductId,
                    SellerEmail = x.SellerEmail,
                    TransactionId = x.TransactionId,
                    TransactionItemId = null,
                    Unit = x.Unit,
                }).ToList();
                var transectionItem = await _transectionItemRepository.getTransactionItemByEmail(sellerEmail);
                foreach (var x in transectionItem)
                {
                    var transectionData = await _transactionRepository.GetSellerTransactionsById(x.TransactionId);
                    result.Add(new ResultBuyer
                    {
                        SellerEmail = x.SellerEmail,
                        Amount = x.Amount,
                        BuyerEmail = transectionData.BuyerEmail,
                        CreateDate = transectionData.CreateDate,
                        Price = x.Price,
                        ProductId = x.ProductId,
                        TransactionId = x.TransactionId,
                        TransactionItemId = x.TransactionItemId,
                        Unit = x.Unit
                    });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public class ResultTransaction : Transaction
        {
            public List<TransectionItems> items { get; set; }

        }
        [HttpGet("get-by-buyer")]
        public async Task<IActionResult> GetTransactionsByBuyer(string buyerEmail)
        {
            try
            {
                var transection = await _transactionRepository.GetBuyerTransactionsByEmail(buyerEmail);
                var transectionItems = new List<ResultTransaction>();
                foreach (var x in transection)
                {
                    var items = await _transectionItemRepository.getTransactionItemById(x.TransactionId);
                    transectionItems.Add(new ResultTransaction
                    {
                        TransactionId = x.TransactionId,
                        Amount = x.Amount,
                        BuyerEmail = x.BuyerEmail,
                        CreateDate = x.CreateDate,
                        Price = x.Price,
                        ProductId = x.ProductId,
                        SellerEmail = x.SellerEmail,
                        Unit = x.Unit,
                        items = items
                    });
                }
                return Ok(transectionItems);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            try
            {
                var transection = await _transactionRepository.GetTransactionById(id);
                var items = await _transectionItemRepository.getTransactionItemById(transection.TransactionId);
               
                return Ok(new ResultTransaction
                {
                    TransactionId = transection.TransactionId,
                    Amount = transection.Amount,
                    BuyerEmail = transection.BuyerEmail,
                    CreateDate = transection.CreateDate,
                    Price = transection.Price,
                    ProductId = transection.ProductId,
                    SellerEmail = transection.SellerEmail,
                    Unit = transection.Unit,
                    items = items
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateTransaction(List<Transaction> transaction)
        {
            try
            {
                var ids = await _transactionRepository.AddTransactions(transaction);
                return Ok(ids);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("create-one")]
        public async Task<IActionResult> CreateTransactions(TransectionModel transaction)
        {
            try
            {
                var id = await _transactionRepository.AddTransaction(new Transaction
                {
                    ProductId = 1,
                    Amount = 1,
                    CreateDate = DateTime.Now,
                    Price = 1,
                    BuyerEmail = transaction.BuyerEmail,
                    SellerEmail = "",
                    Unit = ""
                });
                var ids = await _transectionItemRepository.AddTransactionItem(transaction.items.Select(x => new TransectionItemInsert
                {
                    Unit = x.Unit,
                    Amount = x.Amount,
                    SellerEmail = x.SellerEmail,
                    Price = x.Price,
                    ProductId = x.ProductId,
                    TransectionId = id
                }).ToList());
                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
