using API.Models;
using API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    private ITransactionRepository _transactionRepository;
    public TransactionController(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }
    
    [HttpPost("GetTransactions")]
    public async Task<ActionResult> GetTransactions()
    {
        Response<List<Transaction>> response = new Response<List<Transaction>>()
        {
            code = 200,
            message = "Success",
            data = null
        };
        try
        {
            List<Transaction>? transactions = await _transactionRepository.GetTransaction();
            response.data = transactions;
            return Ok(response);
        }
        catch (Exception e)
        {
            response = new Response<List<Transaction>>()
            {
                code = 500,
                message = "Internal Server Error",
                data = null
            };
            Console.WriteLine(e);
            return Ok(response);
        }
    }
    
    [HttpPost("GetTransactionByAgreement")]
    public async Task<ActionResult> GetTransactionByAgreement(string agreement_number)
    {
        Response<Transaction> response = new Response<Transaction>()
        {
            code = 200,
            message = "Success",
            data = null
        };
        try
        {
            Transaction? transaction = _transactionRepository.GetTransactionByAgreement(agreement_number);
            if (transaction != null)
            {
                response.data = transaction;
            }
            else
            {
                response = new Response<Transaction>()
                {
                    code = 200,
                    message = "Transaction Not Found",
                    data = null
                };
            }
            return Ok(response);
        }
        catch (Exception e)
        {
            response = new Response<Transaction>()
            {
                code = 500,
                message = "Internal Server Error",
                data = null
            };
            Console.WriteLine(e);
            return Ok(response);
        }
    }
    
    [HttpPost("CreateTransaction")]
    public async Task<ActionResult> CreateTransaction(Transaction transaction)
    {
        Response<Transaction> response = new Response<Transaction>()
        {
            code = 200,
            message = "Success",
            data = null
        };
        try
        {
            await _transactionRepository.CreateTransaction(transaction);
            response.data = transaction;
            return Ok(response);
        }
        catch (Exception e)
        {
            response = new Response<Transaction>()
            {
                code = 500,
                message = "Internal Server Error",
                data = null
            };
            Console.WriteLine(e);
            return Ok(response);
        }
    }
}