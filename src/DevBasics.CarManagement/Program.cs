using AutoMapper;
using DevBasics.CarManagement.CarManagement;
using DevBasics.CarManagement.CarRegistration;
using DevBasics.CarManagement.Dependencies;
using DevBasics.CarManagement.Interfaces;
using DevBasics.CarManagement.Services;
using DevBasics.CarManagement.Settings;
using DevBasics.CarManagement.Transaction;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevBasics.CarManagement
{
    internal sealed class Program
    {
        internal static async Task Main()
        {
            var services = new ServiceCollection();

            services.AddTransient<IBulkRegistrationService, BulkRegistrationServiceMock>();
            services.AddTransient<ILeasingRegistrationRepository, LeasingRegistrationRepository>();
            services.AddTransient<ICarRegistrationRepository, CarRegistrationRepository>();
            services.AddTransient<IHaveCustomMappings, CarRegistrationModel>();
            //services.AddAutoMapper();
            services.AddSingleton<ICarManagementSettings, CarManagementSettings>();
            services.AddSingleton<IHttpHeaderSettings, HttpHeaderSettings>();
            services.AddTransient<IKowoLeasingApiClient, KowoLeasingApiClientMock>();
            services.AddTransient<ITransactionStateService, TransactionStateServiceMock>();
            services.AddTransient<IRegistrationDetailService, RegistrationDetailServiceMock>();
            services.AddTransient<IRegisterCarsModel, RegisterCarsModel>();
            services.AddTransient<ICarManagementService, CarManagementService>();
            services.AddScoped<IBeginTransaction, BeginTransaction>();
            services.AddTransient<ICarPoolNumberHelper, CarPoolNumberHelper>();
            services.AddTransient<ICarRegistrationNumberGeneratorFactory, CarRegistrationNumberGeneratorFactory>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //var model = new CarRegistrationModel();
            //var configuration = new MapperConfiguration(cnfgrtn => model.CreateMappings(cnfgrtn));
            //var mapper = configuration.CreateMapper();

            //var bulkRegistrationServiceMock = new BulkRegistrationServiceMock();
            //var leasingRegistrationRepository = new LeasingRegistrationRepository();
            //var carRegistrationRepositoryMock = new CarRegistrationRepository(
            //    leasingRegistrationRepository,
            //    bulkRegistrationServiceMock,
            //    mapper);
            //var beginTransaction = new BeginTransaction(leasingRegistrationRepository, carRegistrationRepositoryMock);
            
            var provider = services.BuildServiceProvider();
            var carManagementService = provider.GetRequiredService<ICarManagementService>();



            //var service = new CarManagementService(
            //    mapper,
            //    new CarManagementSettings(),
            //    new HttpHeaderSettings(),
            //    new KowoLeasingApiClientMock(),
            //    new TransactionStateServiceMock(),
            //    bulkRegistrationServiceMock,
            //    new RegistrationDetailServiceMock(),
            //    leasingRegistrationRepository,
            //    carRegistrationRepositoryMock,
            //    beginTransaction);

            var result = await carManagementService.RegisterCarsAsync(
                new RegisterCarsModel
                {
                    CompanyId = "Company",
                    CustomerId = "Customer",
                    VendorId = "Vendor",
                    Cars = new List<CarRegistrationModel>
                    {
                        new CarRegistrationModel
                        {
                            CompanyId = "Company",
                            CustomerId = "Customer",
                            VehicleIdentificationNumber = Guid.NewGuid().ToString(),
                            DeliveryDate = DateTime.Now.AddDays(14).Date,
                            ErpDeliveryNumber = Guid.NewGuid().ToString()
                        }
                    }
                },
                false,
                new Claims());
        }
    }
}
