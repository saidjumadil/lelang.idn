using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Angular_ASPNETCore_CustomersService.Models;

namespace Angular_ASPNETCore_CustomersService.Repository
{
    public class CustomersRepository : ICustomersRepository
    {

        private readonly CustomersDbContext _Context;
        private readonly ILogger _Logger;

        public CustomersRepository(CustomersDbContext context, ILoggerFactory loggerFactory) {
          _Context = context;
          _Logger = loggerFactory.CreateLogger("CustomersRepository");
        }

        public async Task<List<Barang>> GetCustomersAsync()
        {
            return await _Context.barang.OrderBy(c => c.idBarang).ToListAsync();
        }

        public async Task<PagingResult<Barang>> GetCustomersPageAsync(int skip, int take)
        {
            var totalRecords = await _Context.barang.CountAsync();
            var customers = await _Context.barang
                                 .OrderBy(c => c.idBarang)
                                 .Skip(skip)
                                 .Take(take)
                                 .ToListAsync();
            return new PagingResult<Barang>(customers, totalRecords);
        }

        public async Task<Barang> GetCustomerAsync(int id)
        {
            return await _Context.barang.SingleOrDefaultAsync(c => c.idBarang == id);
        }

        public async Task<Barang> InsertCustomerAsync(Barang customer)
        {
            _Context.Add(customer);
             try
            {
              await _Context.SaveChangesAsync();
            }
            catch (Exception exp)
            {
               _Logger.LogError($"Error in {nameof(UpdateCustomerAsync)}: " + exp.Message);
            }
              return customer;
        }

        public async Task<bool> UpdateCustomerAsync(Barang customer)
        {
            //Will update all properties of the Customer
            _Context.barang.Attach(customer);
            _Context.Entry(customer).State = EntityState.Modified;
            try
            {
              return (await _Context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception exp)
            {
               _Logger.LogError($"Error in {nameof(UpdateCustomerAsync)}: " + exp.Message);
            }
            return false;
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            //Extra hop to the database but keeps it nice and simple for this demo
            //Including orders since there's a foreign-key constraint and we need
            //to remove the orders in addition to the customer
            var customer = await _Context.barang.SingleOrDefaultAsync(c => c.idBarang == id);
            _Context.Remove(customer);
            try
            {
              return (await _Context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (System.Exception exp)
            {
               _Logger.LogError($"Error in {nameof(DeleteCustomerAsync)}: " + exp.Message);
            }
            return false;
        }

    }
}