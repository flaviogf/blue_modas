using System.Collections.Generic;
using System.Linq;
using BlueModas.Api.Database;
using BlueModas.Api.Models;

namespace BlueModas.Api.Repositories
{
    public class EFProductRepository : IProductRepository
    {
        private readonly Context _context;

        public EFProductRepository(Context context)
        {
            _context = context;
        }

        public IList<Product> FindAll()
        {
            return _context.Products.ToList();
        }
    }
}
