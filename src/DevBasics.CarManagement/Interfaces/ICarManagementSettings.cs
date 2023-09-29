using System.Collections.Generic;

namespace DevBasics.CarManagement.Interfaces
{
    public interface ICarManagementSettings
    {
        public IDictionary<int, string> ApiEndpoints { get; set; }
        public IDictionary<string, string> HttpHeaders { get; set; }
        public IDictionary<string, string> LanguageCodes { get; set; }
        public IDictionary<string, int> TimeZones { get; set; }
    }
}