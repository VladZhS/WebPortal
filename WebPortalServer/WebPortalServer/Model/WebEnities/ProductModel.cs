using System;

namespace WebPortalServer.Model.WebEnities
{
    public class ProductModel : BaseModel<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public virtual CategoryModel Category { get; set; }
        public virtual SizeModel Size { get; set; }

        public ProductModel() { }
        public ProductModel(Product product)
        {
            if (product == null)
                throw new ArgumentNullException();
            Id = product.Id;
            Name = product.Name;
            Quantity = product.Quantity;
            Price = product.Price;
            Description = product.Description;
            Category = new CategoryModel(product.Category);
            Size = new SizeModel(product.Size);
        }

        public override Product ToEntity(Product product)
        {
            product.Id = Id;
            product.Name = Name;
            product.Quantity = Quantity;
            product.Price = Price;
            product.Description = Description;
            product.Category = Category.ToEntity(product.Category);
            product.Size = Size.ToEntity(product.Size);
            product.CategoryId = Category.Id;
            product.SizeId = Size.Id;

            return product;
        }
    }
}