using System.Collections.Generic;
using BlueModas.Api.Models;
using BlueModas.Api.ViewModels;

namespace BlueModas.Api.Repositories
{
    public interface IProductRepository
    {
        void Add(Product product);

        IList<Product> FindAll();

        Maybe<Product> FindById(int id);
    }
}
