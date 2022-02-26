using System.Linq;

namespace WebPortalServer.Services
{
    public class DefaultDataService : IDefaultDataService
    {
        private readonly WebPortalDBContext context;

        public DefaultDataService(WebPortalDBContext context)
        {
            this.context = context;
        }
        public void EnsureDefaultData()
        {
            if (context.ProductSize.ToList().Count == 0)
                SetupSizes();
            if (context.OrderStatus.ToList().Count == 0)
                SetupStatuses();
            if (context.ProductCategory.ToList().Count == 0)
                SetupCategories();
        }

        void SetupSizes()
        {
            context.ProductSize.AddRange(
                new ProductSize[]
                {
                    new ProductSize { Size = "Tiny" },
                    new ProductSize { Size = "Small" },
                    new ProductSize { Size = "Medium" },
                    new ProductSize { Size = "Big" },
                    new ProductSize { Size = "Large" },
                });

            context.SaveChanges();
        }

        void SetupStatuses()
        {
            context.OrderStatus.AddRange(
                new OrderStatus[]
                {
                    new OrderStatus { Status = "New" },
                    new OrderStatus { Status = "Processing" },
                    new OrderStatus { Status = "Delivering" },
                    new OrderStatus { Status = "Done" },
                });

            context.SaveChanges();
        }

        void SetupCategories()
        {
            context.ProductCategory.AddRange(
                new ProductCategory[]
                {
                    //new ProductCategory { Category = "" },
                });

            context.SaveChanges();
        }
    }
}
