using API.Context;
using API.Models;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private MyContext _context;
    public TransactionRepository(MyContext context)
    {
        _context = context;
    }
    
    public async Task<List<Transaction>>? GetTransaction()
    {
        try
        {
            List<Transaction>? transactions = await _context.Transactions.Include(l => l.Location).ToListAsync();
            return transactions;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Transaction? GetTransactionByAgreement(string agreement_number)
    {
        try
        {
            Transaction? transaction = _context.Transactions.Where(trx =>
                trx.agreement_number.Equals(agreement_number)).FirstOrDefault();
            return transaction;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task CreateTransaction(Transaction transaction)
    {
        try
        {
            _context.Transactions.Add(transaction);
            var rowsAffected = await _context.SaveChangesAsync();
            if (rowsAffected == 0)
            {
                throw new Exception("No rows affected. Transaction was not saved.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}