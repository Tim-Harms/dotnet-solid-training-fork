using DevBasics.CarManagement.Dependencies;
using DevBasics.CarManagement.Interfaces;
using System;

namespace DevBasics.CarManagement.CarRegistration
{
    public class CarRegistrationNumberGeneratorFactory : ICarRegistrationNumberGeneratorFactory
    {
        public ICarRegistrationNumberGenerator GetCarRegistrationNumberGenerator(CarBrand carBrand)
        {

            switch (carBrand)
            {
                case CarBrand.Ford:
                    return new FordRegistrationNumberGenerator();

                case CarBrand.Toyota:
                    return new ToyotaRegistrationNumberGenerator();

                default:
                    throw new ArgumentOutOfRangeException(nameof(carBrand), carBrand, null);
            }
            //switch (requestOrigin)
            //{
            //    case CarBrand.Ford:
            //        registrationNumber = GenerateFordRegistrationNumber(endCustomerRegistrationReference, registrationRegistrationId);
            //        break;

            //    case CarBrand.Toyota:
            //        registrationNumber = GenerateToyotaRegistrationNumber(endCustomerRegistrationReference, registrationRegistrationId);
            //        break;

            //    case CarBrand.Undefined:
            //        break;

            //    default:
            //        throw new ArgumentOutOfRangeException(nameof(requestOrigin), requestOrigin, null);
            //}
        }
    }
}