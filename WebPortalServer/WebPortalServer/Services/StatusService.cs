using System.Collections.Generic;
using WebPortalServer.Model.WebEnities;

namespace WebPortalServer.Services
{
    public class StatusService : IStatusService
    {

        public OrderStatus CreateStatus(StatusModel status)
        {
            throw new System.NotImplementedException();
        }

        public OrderStatus UpdateStatus(StatusModel status)
        {
            throw new System.NotImplementedException();
        }

        public OrderStatus DeleteStatus(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<StatusModel> ConvertList(IEnumerable<OrderStatus> collection)
        {
            return collection.ConvertModel<OrderStatus, StatusModel>();
        }
    }
}
