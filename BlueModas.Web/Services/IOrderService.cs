using System;
using System.Threading.Tasks;
using BlueModas.Web.Infrastructure;
using BlueModas.Web.ViewModels;

namespace BlueModas.Web.Services
{
    public interface IOrderService
    {
        Task<Result> Add(OrderStoreViewModel order);

        Task<Result> AddItem(Guid orderNumber, OrderItemStoreViewModel item);

        Task<Result> RemoveItem(Guid orderNumber, int productId);

        Task<Result> UpdateItem(Guid orderNumber, int productId, OrderItemUpdateViewModel item);

        Task<Result> AddCustomer(Guid orderNumber, OrderCustomerStoreViewModel customer);

        Task<Result<OrderShowViewModel>> FindByNumber(Guid number);

        Task<Result<int>> CountNumberOfItems(Guid orderNumber);
    }
}
