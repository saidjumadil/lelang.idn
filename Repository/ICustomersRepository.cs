using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Angular_ASPNETCore_CustomersService.Models;

namespace Angular_ASPNETCore_CustomersService.Repository
{
    public interface ICustomersRepository
    {     
        Task<List<Barang>> GetCustomersAsync();
        Task<PagingResult<Barang>> GetCustomersPageAsync(int skip, int take);
        Task<Barang> GetCustomerAsync(int id);
        
        Task<Barang> InsertCustomerAsync(Barang customer);
        Task<bool> UpdateCustomerAsync(Barang customer);
        Task<bool> DeleteCustomerAsync(int id);
    }
}