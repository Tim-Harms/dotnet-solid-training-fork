using DevBasics.CarManagement.Dependencies;
using DevBasics.CarManagement.Interfaces;

namespace DevBasics.CarManagement.Settings
{
    public class HttpHeaderSettings : IHttpHeaderSettings
    {
        public string SalesOrgIdentifier { get; set; }
        public CarBrand WebAppType { get; set; }
    }
}
