using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WebPortalServer
{
    public partial class ProductCategory
    {
        public ProductCategory()
        {
        }

        public int Id { get; set; }
        public string Category { get; set; }

    }
}
