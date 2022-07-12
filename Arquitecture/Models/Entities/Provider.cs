using System;
using System.Collections.Generic;
using Core.Base;
using Core.Ef;

#nullable disable

namespace Models.Entities
{
    public partial class Provider : EntityBase, ISoftDelete
    {
        public Provider()
        {
            Products = new HashSet<Product>();
        }

        public string Nombre { get; set; }
        public string Rnc { get; set; }
        public string Description { get; set; }
        public bool IsDelete { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
