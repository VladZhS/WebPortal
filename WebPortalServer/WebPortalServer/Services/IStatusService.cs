using System.Collections;
using System.Collections.Generic;
using WebPortalServer.Model.WebEnities;

namespace WebPortalServer.Services
{
    public interface IStatusService
    {
        public OrderStatus CreateStatus(StatusModel status);
        public OrderStatus UpdateStatus(StatusModel status);
        public OrderStatus DeleteStatus(int id);
        public IEnumerable<StatusModel> ConvertList(IEnumerable<OrderStatus> collection);
    }
}
