using Microsoft.VisualStudio.TestTools.UnitTesting;
using XmlDemoApplication.Models;
using XmlDemoApplication.Services;

namespace XmlDemoTests
{
    [TestClass]
    public class MainTest
    {
        // In order for tests to work, XML folder + file must be copied to the deployment directory of the test executable (set in .csproj).
        private DataService _service = new DataService();

        [TestMethod]
        public void TestDataLoading()
        {
            DataModel model = _service.GetData();
            Assert.IsTrue(model != null && model.Catalog?.Length > 0, "Error while loading data. Data is null or empty.");
        }

        [TestMethod]
        public void TestBookGenre()
        {
            DataModel model = _service.GetData();

            Assert.AreEqual(model.Catalog[2].Genre, "Fantasy");
        }

        [TestMethod]
        public void TestBookBorrowingSuccess()
        {
            _service.BorrowBook("bk103", "User1");
            (bool success, string msg) operationResult = _service.BorrowBook("bk107", "User2");

            Assert.IsTrue(operationResult.success);
        }

        [TestMethod]
        public void TestBookBorrowingFail()
        {
            _service.BorrowBook("bk101", "User1");
            (bool success, string msg) operationResult = _service.BorrowBook("bk101", "User2");

            Assert.IsFalse(operationResult.success);
        }

        [TestMethod]
        public void TestBookReturningSuccess()
        {
            _service.BorrowBook("bk106", "User5");
            (bool success, string msg) operationResult = _service.ReturnBook("bk106", "User5");

            Assert.IsTrue(operationResult.success);
        }

        [TestMethod]
        public void TestBookReturningFail()
        {
            _service.BorrowBook("bk106", "User5");
            (bool success, string msg) operationResult = _service.ReturnBook("bk106", "User2");

            Assert.IsFalse(operationResult.success);
        }
    }
}
