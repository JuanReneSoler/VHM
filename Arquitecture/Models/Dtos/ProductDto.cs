namespace Models.Dtos
{
    public class ProductDto
    {
        public string Name { get; set; }
        public decimal PriceUnit { get; set; }
        public int ProveedorId { get; set; }
        public int TypeId { get; set; }
        public string Description { get; set; }
    }
}
