using System.Collections.Generic;
using System.Linq;
using GroceryStoreApi.Entities;

namespace GroceryStoreApi.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IStorageContext _StorageContext;
        private ContextData _ContextDataEntities;

        public CustomerRepository(IStorageContext storageContext)
        {
            _StorageContext = storageContext;
            _ContextDataEntities = storageContext.GetData();
        }

        public void Add(Customer entity)
        {
            _ContextDataEntities.customers.Add(entity);
        }

        public List<Customer> GetAllCustomer()
        {
            return _ContextDataEntities.customers;
        }

        public Customer GetCustomer(int id)
        {
            return _ContextDataEntities.customers.FirstOrDefault(s => s.Id == id);
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
