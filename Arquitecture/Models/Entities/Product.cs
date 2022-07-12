using System;
using System.Collections.Generic;
using Core.Base;

#nullable disable

namespace Models.Entities
{
    public partial class Product : EntityBase
    {
        public string Name { get; set; }
        public decimal PriceUnit { get; set; }
        public int ProveedorId { get; set; }
        public int TypeId { get; set; }
        public string Description { get; set; }

        public virtual Provider Proveedor { get; set; }
        public virtual ProductsType Type { get; set; }
    }
}
