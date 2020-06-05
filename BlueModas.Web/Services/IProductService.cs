using System.Collections.Generic;
using System.Threading.Tasks;
using BlueModas.Web.ViewModels;

namespace BlueModas.Web.Services
{
    public interface IProductService
    {
        Task<IList<ProductIndexViewModel>> FindAll();
    }
}
