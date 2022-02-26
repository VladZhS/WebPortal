using System;

namespace WebPortalServer.Model.WebEnities
{
    public class SizeModel : BaseModel<ProductSize>
    {
        public int Id { get; set; }
        public string Size { get; set; }
        public SizeModel() { }
        public SizeModel(ProductSize model)
        {
            if (model == null)
                throw new ArgumentNullException();
            Id = model.Id;
            Size = model.Size;
        }

        public override ProductSize ToEntity(ProductSize product)
        {
            product.Id = Id;
            product.Size = Size;
            return product;
        }
    }
}
