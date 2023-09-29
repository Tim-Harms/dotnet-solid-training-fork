using DevBasics.CarManagement.Dependencies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevBasics.CarManagement.Transaction
{
    public interface IBeginTransaction
    {
        Task<string> BeginTransactionGenerateId(IList<string> cars,
            string customerId, string companyId, RegistrationType registrationType, string identity, string registrationNumber = null);

        Task<string> BeginTransactionAsync(IList<string> cars,
            string customerId, string companyId, RegistrationType registrationType, string identity,
            string transactionId = null, string registrationNumber = null);
    }
}