using System;
using System.IO.Abstractions;
using AutoFixture;
using GroceryStoreApi.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GroceryStoreApiTest.DataTest
{
    [TestClass]
    public class JsonStorageContextTest
    {

        [TestMethod]
        public void GetDataTest_1()
        {
            var fixture = new Fixture();
            var filesystem = fixture.Freeze<Mock<IFileSystem>>();
            filesystem.Setup(f => f.File.Exists(It.IsAny<string>())).Returns(true);
            filesystem.Setup(f => f.File.ReadAllText(It.IsAny<string>())).Returns(() => "{ \"customers\": [{\"id\": 1,\"name\": \"Bob\"},{\"id\": 2, \"name\": \"Mary\"}, {\"id\": 3,\"name\": \"Joe\"}]}");

            JsonStorageContext jsonStorageContext = new JsonStorageContext(fixture.Create<string>(), filesystem.Object);

            jsonStorageContext.Read().Wait();

            var contextData = jsonStorageContext.GetData();

            Assert.IsTrue(contextData.customers.Count == 3);
        }


        [TestMethod]
        public void GetDataTest_2()
        {
            var fixture = new Fixture();
            var filesystem = fixture.Freeze<Mock<IFileSystem>>();
            filesystem.Setup(f => f.File.Exists(It.IsAny<string>())).Returns(false);

            filesystem.Setup(f => f.File.ReadAllText(It.IsAny<string>())).Returns(() => "{ \"customers\": [{\"id\": 1,\"name\": \"Bob\"},{\"id\": 2, \"name\": \"Mary\"}, {\"id\": 3,\"name\": \"Joe\"}]}");

            JsonStorageContext jsonStorageContext = new JsonStorageContext(fixture.Create<string>(), filesystem.Object);
            jsonStorageContext.Read().Wait();

            var contextData = jsonStorageContext.GetData();

            Assert.IsNull(contextData.customers);
        }

        [TestMethod]
        public void GetDataTest_3()
        {
            var fixture = new Fixture();
            var filesystem = fixture.Freeze<Mock<IFileSystem>>();
            filesystem.Setup(f => f.File.Exists(It.IsAny<string>())).Returns(true);
            filesystem.Setup(f => f.File.ReadAllText(It.IsAny<string>())).Returns(() => "{ [{\"id\": 1,\"name\": \"Bob\"},{\"id\": 2, \"name\": \"Mary\"}, {\"id\": 3,\"name\": \"Joe\"}]}");

            JsonStorageContext jsonStorageContext = new JsonStorageContext(fixture.Create<string>(), filesystem.Object);

            Assert.ThrowsException<AggregateException>(() => jsonStorageContext.Read().Wait());
        }
    }
}
