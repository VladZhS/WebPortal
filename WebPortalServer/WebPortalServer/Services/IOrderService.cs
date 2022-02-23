using System.Collections.Generic;
using WebPortalServer.Model.WebEnities;

namespace WebPortalServer.Services
{
    public interface IOrderService
    {
        public Order CreateOrder(OrderModel model);
        public Order UpdateOrder(OrderModel model);
        public Order DeleteOrder(int id);
        public Order ArchiveOrder(int id);
        public Order UnarchiveOrder(int id);
        public IEnumerable<OrderModel> ConvertList(IEnumerable<Order> collection);
    }
}
