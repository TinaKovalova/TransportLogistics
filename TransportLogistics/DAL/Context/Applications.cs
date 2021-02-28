namespace DAL.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Applications
    {
        [Key]
        public int ApplicationId { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(50)]
        public string FromWhere { get; set; }

        [Required]
        [StringLength(50)]
        public string ToWhere { get; set; }

        [Required]
        [StringLength(50)]
        public string Note { get; set; }

        public int? ApplicationStatusId { get; set; }

        public int? UserId { get; set; }

        public virtual ApplicationStatus ApplicationStatus { get; set; }

        public virtual Users Users { get; set; }
    }
}
