namespace DAL.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cars
    {
        [Key]
        public int CarId { get; set; }

        [Required]
        [StringLength(20)]
        public string CarName { get; set; }

        [Required]
        [StringLength(20)]
        public string CarNumber { get; set; }

        public decimal FuelConsumption { get; set; }

        public int? FuelId { get; set; }

        public int? DriverId { get; set; }

        public virtual Users Users { get; set; }

        public virtual Fuel Fuel { get; set; }
    }
}
