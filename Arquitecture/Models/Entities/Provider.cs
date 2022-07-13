using Core.Base;

#nullable disable

namespace Models.Entities
{
    public partial class Provider : EntityBase
    {
        public Provider()
        {
            Products = new HashSet<Product>();
        }

        public string Nombre { get; set; }
        public string Rnc { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
