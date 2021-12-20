using XmlDemoApplication.Models;

namespace XmlDemoApplication.Services
{
    public interface IDataService
    {
        public DataModel GetData();

        public (bool, string) BorrowBook(string bookId, string username);

        public (bool, string) ReturnBook(string bookId, string username);
    }
}
