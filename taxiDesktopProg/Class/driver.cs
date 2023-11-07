namespace taxiDesktopProg
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("driver")]
    public partial class driver
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public driver()
        {
            orders = new HashSet<order>();
            violations = new HashSet<violation>();
            work_schedule = new HashSet<work_schedule>();
        }

        [Key]
        public long id_driver { get; set; }

        [Required]
        [StringLength(100)]
        public string status { get; set; }

        [Required]
        [StringLength(100)]
        public string driver_readiness { get; set; }

        [Required]
        [StringLength(10)]
        public string call_sign { get; set; }

        [Required]
        [StringLength(150)]
        public string surname { get; set; }

        [Required]
        [StringLength(150)]
        public string name { get; set; }

        [StringLength(150)]
        public string patronymic { get; set; }

        [Column(TypeName = "date")]
        public DateTime date_of_birth { get; set; }

        [StringLength(12)]
        public string drivers_license_number { get; set; }

        [Required]
        [StringLength(11)]
        public string mobile_phone { get; set; }

        public long? id_car { get; set; }

        public virtual car car { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order> orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<violation> violations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<work_schedule> work_schedule { get; set; }
    }
}
