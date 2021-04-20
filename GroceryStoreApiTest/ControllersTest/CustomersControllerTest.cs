using System;
using System.Collections.Generic;
using AutoFixture;
using GroceryStoreApi.Controllers;
using GroceryStoreApi.Data;
using GroceryStoreApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GroceryStoreApiTest.ControllersTest
{
    [TestClass]
    public class CustomersControllerTest
    {

        [TestMethod]
        public void GetCustomers_01()
        {
            var fixture = new Fixture();
            var mockRepo = fixture.Freeze<Mock<ICustomerRepository>>();

            mockRepo.Setup(m => m.GetAllCustomer()).Returns(new List<Customer>() { new Customer() { Id = 1, Name = "Mary" }, new Customer() { Id = 2, Name = "John" } });

            var controller = new CustomersController(mockRepo.Object);

            var customers = controller.GetCustomers();

            Assert.IsNotNull(customers.Result);
            Assert.IsInstanceOfType(customers.Result, typeof(OkObjectResult));
            Assert.IsTrue(((OkObjectResult)customers.Result).StatusCode == StatusCodes.Status200OK);

            var result = (List<Customer>)((OkObjectResult)customers.Result).Value;

            Assert.IsTrue(result.Count == 2);
        }

        [TestMethod]
        public void GetCustomers_02()
        {
            var fixture = new Fixture();
            var mockRepo = fixture.Freeze<Mock<ICustomerRepository>>();

            mockRepo.Setup(m => m.GetAllCustomer()).Throws(new Exception());

            var controller = new CustomersController(mockRepo.Object);

            var customers = controller.GetCustomers();

            Assert.IsNotNull(customers.Result);
            Assert.IsInstanceOfType(customers.Result, typeof(ObjectResult));
            Assert.IsTrue(((ObjectResult)customers.Result).StatusCode == StatusCodes.Status500InternalServerError);
        }

        [TestMethod]
        public void GetCustomer_01()
        {
            var fixture = new Fixture();
            var mockRepo = fixture.Freeze<Mock<ICustomerRepository>>();

            mockRepo.Setup(m => m.GetCustomer(It.IsAny<int>())).Returns(new Customer() { Id = 1, Name = "Mary" });

            var controller = new CustomersController(mockRepo.Object);

            var customers = controller.GetCustomer(fixture.Create<int>());

            Assert.IsNotNull(customers.Result);
            Assert.IsInstanceOfType(customers.Result, typeof(OkObjectResult));
            Assert.IsTrue(((OkObjectResult)customers.Result).StatusCode == StatusCodes.Status200OK);

            var result = (Customer)((OkObjectResult)customers.Result).Value;

            Assert.IsTrue(result.Name == "Mary");
        }


        [TestMethod]
        public void Update_01()
        {
            var fixture = new Fixture();
            var mockRepo = fixture.Freeze<Mock<ICustomerRepository>>();

            mockRepo.Setup(m => m.Update(It.IsAny<Customer>())).Returns(new Customer() { Id = 1, Name = "Mary" });

            var controller = new CustomersController(mockRepo.Object);

            var customers = controller.UpdateCustomer(fixture.Create<Customer>());

            Assert.IsNotNull(customers.Result);
            Assert.IsInstanceOfType(customers.Result, typeof(OkObjectResult));
            Assert.IsTrue(((OkObjectResult)customers.Result).StatusCode == StatusCodes.Status200OK);

            var result = (Customer)((OkObjectResult)customers.Result).Value;

            Assert.IsTrue(result.Name == "Mary");
        }
    }
}
