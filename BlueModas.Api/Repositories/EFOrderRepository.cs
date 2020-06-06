using System;
using System.Linq;
using BlueModas.Api.Database;
using BlueModas.Api.Infrastructure;
using BlueModas.Api.Models;

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

        public Maybe<Order> FindByNumber(Guid number)
        {
            return _context.Orders.FirstOrDefault(it => it.Number == number);
        }
    }
}
