using System.Collections.Generic;
using BlueModas.Api.Models;

namespace BlueModas.Api.Repositories
{
    public interface IProductRepository
    {
        void Add(Product product);

        IList<Product> FindAll();
    }
}
