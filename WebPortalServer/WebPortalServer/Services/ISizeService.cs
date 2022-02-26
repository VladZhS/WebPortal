

using System.Collections.Generic;
using WebPortalServer.Model.WebEnities;

namespace WebPortalServer.Services
{
    public interface ISizeService
    {
        public ProductSize CreateSize(SizeModel model);
        public ProductSize UpdateSize(SizeModel model);
        public ProductSize DeleteSize(int id);
        public IEnumerable<SizeModel> ConvertList(IEnumerable<ProductSize> collection);
    }
}
