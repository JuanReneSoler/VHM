using Core.Base;

#nullable disable

namespace Models.Entities
{
    public partial class ProductsType : EntityBase
    {
        public ProductsType()
        {
            Products = new HashSet<Product>();
        }

        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
