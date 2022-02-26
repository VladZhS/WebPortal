namespace WebPortalServer.Model.WebEnities
{
    public class StatusModel : BaseModel<OrderStatus>
    {
        public int Id { get; set; }
        public string Status { get; set; }

        public StatusModel() { }
        public StatusModel(OrderStatus status)
        {
            Id = status.Id;
            Status = status.Status;
        }
        public override OrderStatus ToEntity(OrderStatus order)
        {
            order.Id = Id;
            order.Status = Status;
            return order;
        }
        
    }
}
