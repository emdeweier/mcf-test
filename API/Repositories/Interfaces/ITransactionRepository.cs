using API.Models;

namespace API.Repositories.Interfaces;

public interface ITransactionRepository
{
    Task<List<Transaction>>? GetTransaction();
    Transaction? GetTransactionByAgreement(string agreement_number);
    Task CreateTransaction(Transaction transaction);
}