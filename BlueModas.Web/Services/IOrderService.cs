using System.Threading.Tasks;
using BlueModas.Web.Infrastructure;
using BlueModas.Web.ViewModels;

namespace BlueModas.Web.Services
{
    public interface IOrderService
    {
        Task<Result> Create(OrderStoreViewModel order);
    }
}
