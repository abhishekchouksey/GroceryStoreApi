using System.Collections.Generic;
using GroceryStoreApi.Entities;

namespace GroceryStoreApi.Data
{
    public interface ICustomerRepository
    {
        // General 
        void Add(Customer entity);
        void SaveChanges();

        // Customers
        List<Customer> GetAllCustomer();
        Customer GetCustomer(int id);
        Customer Update(Customer customer);
    }
}
