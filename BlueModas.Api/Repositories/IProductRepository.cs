using System.Collections.Generic;
using BlueModas.Api.Infrastructure;
using BlueModas.Api.Models;

namespace BlueModas.Api.Repositories
{
    public interface IProductRepository
    {
        void Add(Product product);

        void Remove(Product product);

        IList<Product> FindAll();

        Maybe<Product> FindById(int id);
    }
}
