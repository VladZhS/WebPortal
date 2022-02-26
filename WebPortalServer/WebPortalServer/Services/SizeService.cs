using System.Collections.Generic;
using WebPortalServer.Model.WebEnities;

namespace WebPortalServer.Services
{
    public class SizeService : ISizeService
    {
        public ProductSize CreateSize(SizeModel model)
        {
            throw new System.NotImplementedException();
        }

        public ProductSize DeleteSize(int id)
        {
            throw new System.NotImplementedException();
        }

        public ProductSize UpdateSize(SizeModel model)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<SizeModel> ConvertList(IEnumerable<ProductSize> collection)
        {
            return collection.ConvertModel<ProductSize, SizeModel>();
        }
    }
}
