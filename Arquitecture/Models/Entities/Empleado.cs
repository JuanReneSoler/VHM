using System;
using System.Collections.Generic;
using Core.Base;
using Core.Ef;

#nullable disable

namespace Models.Entities
{
    public partial class Empleado : EntityBase, ISoftDelete
    {
        public string Names { get; set; }
        public string Surnames { get; set; }
        public string UserId { get; set; }
        public DateTime Birthday { get; set; }
        public string DocId { get; set; }
        public bool IsDelete { get; set; }
        public string FullName { get; set; }
    }
}
