using System.Collections.Generic;
using BlueModas.Api.Models;

namespace BlueModas.Api.Repositories
{
    public interface IProductRepository
    {
        IList<Product> FindAll();
    }
}
