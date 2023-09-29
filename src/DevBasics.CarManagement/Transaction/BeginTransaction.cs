using AutoMapper;
using DevBasics.CarManagement.CarManagement;
using DevBasics.CarManagement.Dependencies;
using DevBasics.CarManagement.Interfaces;
using DevBasics.CarManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBasics.CarManagement.Transaction
{
    public class BeginTransaction : IBeginTransaction
    {
        public ICarRegistrationRepository CarRegistrationRepository { get; set; } // Umbenannt von CarLeasingRepository -> Liskov
        public ILeasingRegistrationRepository LeasingRegistrationRepository { get; set; }

        public BeginTransaction(
            ILeasingRegistrationRepository leasingRegistrationRepository, // Umbenannt von registrationRepository -> Liskov
            ICarRegistrationRepository carRegistrationRepository)
        {
            Console.WriteLine($"Initializing class {nameof(BeginTransaction)}");
            CarRegistrationRepository = carRegistrationRepository;
            LeasingRegistrationRepository = leasingRegistrationRepository;
        }

        public async Task<string> BeginTransactionGenerateId(IList<string> cars,
            string customerId, string companyId, RegistrationType registrationType, string identity, string registrationNumber = null)
        {
            Console.WriteLine(
                $"Trying to generate internal database transaction and initialize the transaction. Cars: {string.Join(",  ", cars)} ");

            try
            {
                string transactionId = DateTime.Now.Ticks.ToString();
                if (transactionId.Length > 32)
                {
                    transactionId = transactionId.Substring(0, 32);
                }

                return await BeginTransactionAsync(cars, customerId, companyId, registrationType, identity, transactionId, registrationNumber);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Generating internal Transaction ID and initializing transaction failed. Cars: {string.Join(", ", cars)}: {ex}");

                throw ex;
            }
        }

        public async Task<string> BeginTransactionAsync(IList<string> cars,
            string customerId, string companyId, RegistrationType registrationType, string identity,
            string transactionId = null, string registrationNumber = null)
        {
            Console.WriteLine(
                $"Trying to begin internal database transaction. Cars: {string.Join(",  ", cars)}");

            try
            {
                IList<CarRegistrationDto> dbCarsToUpdate = await CarRegistrationRepository.GetCarsAsync(cars);
                foreach (CarRegistrationDto carToUpdate in dbCarsToUpdate)
                {
                    if (!string.IsNullOrWhiteSpace(transactionId))
                    {
                        carToUpdate.TransactionId = transactionId;
                    }

                    if (!string.IsNullOrWhiteSpace(registrationNumber))
                    {
                        carToUpdate.CarPoolNumber = registrationNumber;
                    }

                    carToUpdate.TransactionEndDate = null;
                    carToUpdate.ErrorMessage = string.Empty;
                    carToUpdate.ErrorCode = null;

                    carToUpdate.TransactionType = (int)registrationType;
                    //carToUpdate.TransactionState = (int)TransactionResult.Progress;
                    carToUpdate.TransactionState = carToUpdate.TransactionState ?? (int)TransactionResult.NotRegistered;

                    Console.WriteLine(
                        $"Car hasn't got missing data. Setting status to {carToUpdate.TransactionState}");

                    carToUpdate.TransactionStartDate = DateTime.Now;

                    Console.WriteLine(
                        $"Trying to update car {carToUpdate.CarIdentificationNumber} in database...");

                    await LeasingRegistrationRepository.UpdateCarAsync(carToUpdate);
                    await LeasingRegistrationRepository.InsertHistoryAsync(carToUpdate,
                        identity,
                        carToUpdate.TransactionState.HasValue ? Enum.GetName(typeof(TransactionResult), (int)carToUpdate.TransactionState) : null,
                        carToUpdate.TransactionType.HasValue ? Enum.GetName(typeof(RegistrationType), (int)carToUpdate.TransactionType) : null
                    );
                }

                Console.WriteLine(
                        $"Beginning internal database transaction ended. Cars: {string.Join(",  ", cars)}, " +
                        $"Returning internal Transaction ID: {transactionId}");

                return transactionId;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Beginning internal database transaction failed. Cars: {string.Join(",  ", cars)}: {ex}");

                throw new Exception("Beginning internal database transaction failed", ex);
            }
        }
    }
}
