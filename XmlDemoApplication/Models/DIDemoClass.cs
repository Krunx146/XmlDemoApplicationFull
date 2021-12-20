using XmlDemoApplication.Services;

namespace XmlDemoApplication.Models
{
    public class DIDemoClass
    {
        private IDataService _dataService;

        public DIDemoClass(IDataService dataService)
        {
            _dataService = dataService;
        }

        public Book[] GetDemoData()
        {
            return _dataService.GetData().Catalog;
        }
    }
}
