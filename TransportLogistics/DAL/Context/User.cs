namespace DAL.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Car = new HashSet<Car>();
            Order = new HashSet<Order>();
        }

        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string UserLastName { get; set; }

        [Required]
        [StringLength(50)]
        public string UserFirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string UserPatronymic { get; set; }

        [Required]
        [StringLength(20)]
        public string UserDrivingLecense { get; set; }

        public int? RoleId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Car> Car { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Order { get; set; }

        public virtual Role Role { get; set; }
    }
}
