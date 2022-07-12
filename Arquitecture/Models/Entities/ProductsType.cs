using System;
using System.Collections.Generic;
using Core.Base;
using Core.Ef;

#nullable disable

namespace Models.Entities
{
    public partial class ProductsType : EntityBase, ISoftDelete
    {
        public ProductsType()
        {
            Products = new HashSet<Product>();
        }

        public string Description { get; set; }
        public bool IsDelete { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
