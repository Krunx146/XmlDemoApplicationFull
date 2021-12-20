using System.IO;
using System.Linq;
using System.Xml.Serialization;
using XmlDemoApplication.Models;

namespace XmlDemoApplication.Services
{
    public class DataService : IDataService
    {
        private DataModel _dataStore;

        private string filePath;

        public DataService(string xmlFilePath = "XML\\Data.xml")
        {
            filePath = xmlFilePath;
        }

        public DataModel GetData()
        {
            if (_dataStore == null)
            {
                _dataStore = ReadXmlData();
            }
            return _dataStore;
        }

        public (bool, string) BorrowBook(string bookId, string username)
        {
            Book book = GetData().Catalog?.ToList().Find(x => x.Id == bookId);

            if (book == null)
            {
                return (false, "Knjiga nije pronađena u kolekciji!");
            }
            else if (book.IsBorrowed)
            {
                return (false, $"Knjiga je već posuđena od strane: {book.BorrowedBy}.");
            }

            book.BorrowedBy = username;
            return (true, "Knjiga je posuđena.");
        }

        public (bool, string) ReturnBook(string bookId, string username)
        {
            Book book = GetData().Catalog?.ToList().Find(x => x.Id == bookId);

            if (book == null)
            {
                return (false, "Knjiga nije pronađena u kolekciji!");
            }
            else if (!book.IsBorrowed)
            {
                return (false, $"Knjiga nije posuđena.");
            }
            else if (book.BorrowedBy != username)
            {
                return (false, $"Nije moguće vratiti knjigu koju niste posudili.");
            }

            book.BorrowedBy = string.Empty;
            return (true, "Knjiga je vraćena.");
        }

        private DataModel ReadXmlData()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(DataModel));

            using Stream reader = new FileStream(Path.GetFullPath(filePath), FileMode.Open);
            
            return (DataModel)serializer.Deserialize(reader);
        }
    }
}
