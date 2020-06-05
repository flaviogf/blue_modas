using System.Collections.Generic;
using System.Linq;
using BlueModas.Api.Database;
using BlueModas.Api.Models;
using BlueModas.Api.ViewModels;

namespace BlueModas.Api.Repositories
{
    public class EFProductRepository : IProductRepository
    {
        private readonly Context _context;

        public EFProductRepository(Context context)
        {
            _context = context;
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
        }

        public void Remove(Product product)
        {
            _context.Products.Remove(product);
        }

        public IList<Product> FindAll()
        {
            return _context.Products.ToList();
        }

        public Maybe<Product> FindById(int id)
        {
            return _context.Products.FirstOrDefault(it => it.Id == id);
        }
    }
}
