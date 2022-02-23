using WebPortalServer.Model.WebEnities;

namespace WebPortalServer.Services.Validators
{
    public interface IOrderValidator
    {
        public ModelError IsValid(OrderModel order);
    }
}
