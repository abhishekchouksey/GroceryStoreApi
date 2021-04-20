using System.Collections.Generic;
using System.Linq;
using GroceryStoreApi.Entities;
using Microsoft.Extensions.Logging;

namespace GroceryStoreApi.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        private ILogger<CustomerRepository> _Logger;
        private readonly IStorageContext _StorageContext;
        public virtual ContextData _ContextEntities { get; set; }

        public CustomerRepository(ILogger<CustomerRepository> logger, IStorageContext storageContext)
        {
            _Logger = logger;
            _StorageContext = storageContext;
            _ContextEntities = storageContext.GetData();
        }

        public void Add(Customer entity)
        {
            _ContextEntities.customers.Add(entity);
        }

        public List<Customer> GetAllCustomer()
        {
            return _ContextEntities.customers;
        }

        public Customer GetCustomer(int id)
        {
            return _ContextEntities.customers.FirstOrDefault(s => s.Id == id);
        }

        public void SaveChanges()
        {
            _StorageContext.Save();
        }

        public Customer Update(Customer customer)
        {
            // TODO : PERFORM ERROR HANDLING
            Customer customerToBeUpdated = GetCustomer(customer.Id);
            customerToBeUpdated.Name = customer.Name;
            SaveChanges();

            return customer;
        }
    }
}
