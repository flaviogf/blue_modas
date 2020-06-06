using System.Collections.Generic;
using System.Threading.Tasks;
using BlueModas.Web.Infrastructure;
using BlueModas.Web.ViewModels;

namespace BlueModas.Web.Services
{
    public interface IProductService
    {
        Task<Result<IList<ProductIndexViewModel>>> FindAll();
    }
}
