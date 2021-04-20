using System.Collections.Generic;
using AutoFixture;
using GroceryStoreApi.Data;
using GroceryStoreApi.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GroceryStoreApiTest.DataTest
{
    [TestClass]
    public class CustomerRepositoryTest
    {
        [TestMethod]
        public void Add_And_Get_1()
        {
            var fixture = new Fixture();
            var storageContextMock = fixture.Freeze<Mock<IStorageContext>>();

            storageContextMock.Setup(s => s.GetData()).Returns(new ContextData() { customers = new List<Customer>() { new Customer() { Id = 1, Name = "Mary" }, new Customer() { Id = 2, Name = "John" } } });

            var customerRepository = new CustomerRepository(storageContextMock.Object);

            customerRepository.Add(new Customer() { Id = 3, Name = "Hank" });

            var customer = customerRepository.GetCustomer(3);

            storageContextMock.Verify(s => s.GetData(), Times.Once);
            Assert.IsNotNull(customer);
        }


        [TestMethod]
        public void Add_And_Get_2()
        {
            var fixture = new Fixture();
            var storageContextMock = fixture.Freeze<Mock<IStorageContext>>();

            storageContextMock.Setup(s => s.GetData()).Returns(new ContextData() { customers = new List<Customer>() { new Customer() { Id = 1, Name = "Mary" }, new Customer() { Id = 2, Name = "John" } } });

            var customerRepository = new CustomerRepository(storageContextMock.Object);

            customerRepository.Add(new Customer() { Id = 3, Name = "Hank" });

            var customers = customerRepository.GetAllCustomer();

            storageContextMock.Verify(s => s.GetData(), Times.Once);
            Assert.IsNotNull(customers);
            Assert.IsTrue(customers.Count == 3);
        }
    }
}
