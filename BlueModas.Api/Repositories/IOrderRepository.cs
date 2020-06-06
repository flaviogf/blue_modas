using System;
using BlueModas.Api.Infrastructure;
using BlueModas.Api.Models;

namespace BlueModas.Api.Repositories
{
    public interface IOrderRepository
    {
        void Add(Order order);
        
        Maybe<Order> FindByNumber(Guid number);
    }
}
