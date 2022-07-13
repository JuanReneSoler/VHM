using Core.Base;

#nullable disable

namespace Models.Entities
{
    public partial class Empleado : EntityBase
    {
        public string Names { get; set; }
        public string Surnames { get; set; }
        public string UserId { get; set; }
        public DateTime Birthday { get; set; }
        public string DocId { get; set; }
        public string FullName { get; set; }
    }
}
