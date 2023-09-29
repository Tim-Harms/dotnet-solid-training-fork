using DevBasics.CarManagement.CarRegistration;
using DevBasics.CarManagement.Dependencies;
using DevBasics.CarManagement.Interfaces;
using System;

namespace DevBasics.CarManagement.Services
{
    public class CarPoolNumberHelper : ICarPoolNumberHelper
    {
        private readonly ICarRegistrationNumberGeneratorFactory CarRegistrationNumberGeneratorFactory;
        public CarPoolNumberHelper(ICarRegistrationNumberGeneratorFactory carRegistrationNumberGeneratorFactory)
        {
            CarRegistrationNumberGeneratorFactory = carRegistrationNumberGeneratorFactory;
        }

        public void Generate(CarBrand requestOrigin, string endCustomerRegistrationReference, out string registrationRegistrationId, out string registrationNumber)
        {
            registrationRegistrationId = GenerateRegistrationRegistrationId();
            ICarRegistrationNumberGenerator carRegistrationNumberGenerator = CarRegistrationNumberGeneratorFactory.GetCarRegistrationNumberGenerator(requestOrigin);
            registrationNumber = carRegistrationNumberGenerator.GenerateCarRegistrationNumber(endCustomerRegistrationReference, registrationRegistrationId);
        }

        public string GenerateRegistrationRegistrationId()
        {
            return DateTime.Now.Ticks.ToString();
        }
    }
}