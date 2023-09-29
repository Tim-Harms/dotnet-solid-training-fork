using DevBasics.CarManagement.Dependencies;

namespace DevBasics.CarManagement.Interfaces
{
    public interface IHttpHeaderSettings
    {
        public string SalesOrgIdentifier { get; set; }
        public CarBrand WebAppType { get; set; }
    }
}