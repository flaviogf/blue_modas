using System;
using System.Collections.Generic;
using System.Linq;
using BlueModas.Api.Database;
using BlueModas.Api.Infrastructure;
using BlueModas.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BlueModas.Api.Repositories
{
    public class EFOrderRepository : IOrderRepository
    {
        private readonly Context _context;

        public EFOrderRepository(Context context)
        {
            _context = context;
        }

        public void Add(Order order)
        {
            _context.Orders.Add(order);
        }

        public IList<Order> FindAll()
        {
            return _context.Orders.ToList();
        }

        public Maybe<Order> FindByNumber(Guid number)
        {
            return _context.Orders
                .Include(it => it.Items)
                .FirstOrDefault(it => it.Number == number);
        }
    }
}
