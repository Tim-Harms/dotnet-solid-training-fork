using DevBasics.CarManagement.Dependencies;
using System.Threading.Tasks;

namespace DevBasics.CarManagement.Interfaces
{
    public interface ICarRegistrationNumberGenerator
    {
        public string GenerateCarRegistrationNumber(string endCustomerRegistrationReference, string registrationRegistrationId);
    }
}
