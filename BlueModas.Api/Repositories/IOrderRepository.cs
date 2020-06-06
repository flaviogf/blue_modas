using System;
using System.Collections.Generic;
using BlueModas.Api.Infrastructure;
using BlueModas.Api.Models;

namespace BlueModas.Api.Repositories
{
    public interface IOrderRepository
    {
        void Add(Order order);

        IList<Order> FindAll();
        
        Maybe<Order> FindByNumber(Guid number);
    }
}
