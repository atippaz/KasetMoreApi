using KasetMore.Data.Models;
using KasetMore.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KasetMore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpGet("get-by-seller")]
        public async Task<IActionResult> GetTransactionsBySeller(string sellerEmail)
        {
            try
            {
                return Ok(await _transactionRepository.GetSellerTransactionsByEmail(sellerEmail));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("get-by-buyer")]
        public async Task<IActionResult> GetTransactionsByBuyer(string buyerEmail)
        {
            try
            {
                return Ok(await _transactionRepository.GetBuyerTransactionsByEmail(buyerEmail));
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
                return Ok(await _transactionRepository.GetTransactionById(id));
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
                var ids = await _transactionRepository.AddTransaction(transaction);
                return Ok(ids);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
