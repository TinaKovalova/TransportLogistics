namespace DAL.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        public int OrderId { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(50)]
        public string WhereFrom { get; set; }

        [Required]
        [StringLength(50)]
        public string Where { get; set; }

        [StringLength(50)]
        public string Note { get; set; }

        public int? CarId { get; set; }

        public int? StatusId { get; set; }

        public int? UserId { get; set; }

        public virtual Car Car { get; set; }

        public virtual OrderStatus OrderStatus { get; set; }

        public virtual User User { get; set; }
    }
}
