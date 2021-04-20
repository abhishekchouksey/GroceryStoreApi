using System;
using System.Collections.Generic;
using GroceryStoreApi.Data;
using GroceryStoreApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStoreApi.Controllers
{
    [Route("api/[Controller]")]
    public class CustomersController : ControllerBase
    {
        private ICustomerRepository _CustomerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _CustomerRepository = customerRepository;
        }

        [HttpGet]
        public ActionResult<List<Customer>> GetCustomers()
        {
            try
            {
                return Ok(_CustomerRepository.GetAllCustomer());
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "something terrible happend!!");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomer(int id)
        {
            try
            {
                return Ok(_CustomerRepository.GetCustomer(id));
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "something terrible happend!!");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Customer> UpdateCustomer([FromBody] Customer customer)
        {
            try
            {
                return Ok(_CustomerRepository.Update(customer));
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "something terrible happend!!");
            }
        }
    }
}
