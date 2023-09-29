using DevBasics.CarManagement.Dependencies;
using DevBasics.CarManagement.Interfaces;
using System;

namespace DevBasics.CarManagement.CarRegistration
{
    public class FordRegistrationNumberGenerator : ICarRegistrationNumberGenerator
    {

        public string GenerateCarRegistrationNumber(string endCustomerRegistrationReference, string registrationRegistrationId)
        {
            return string.IsNullOrWhiteSpace(endCustomerRegistrationReference) ? registrationRegistrationId : endCustomerRegistrationReference;
        }
    }
}