using DevBasics.CarManagement.Dependencies;
using System.Threading.Tasks;

namespace DevBasics.CarManagement.Interfaces
{
    public interface ICarRegistrationNumberGeneratorFactory
    {
        public ICarRegistrationNumberGenerator GetCarRegistrationNumberGenerator(CarBrand carBrand);
    }
}
