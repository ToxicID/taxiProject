namespace taxiDesktopProg
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("car")]
    public partial class car
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public car()
        {
            drivers = new HashSet<driver>();
        }

        [Key]
        public long id_car { get; set; }

        public bool rented_car { get; set; }

        [Required]
        [StringLength(150)]
        public string colour { get; set; }

        [Required]
        [StringLength(200)]
        public string car_brand { get; set; }

        [Required]
        [StringLength(200)]
        public string car_model { get; set; }

        [StringLength(6)]
        public string state_number { get; set; }

        [Required]
        [StringLength(3)]
        public string region_code { get; set; }

        [Required]
        [StringLength(50)]
        public string technical_condition_car { get; set; }

        public long id_car_category { get; set; }

        public virtual car_category car_category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<driver> drivers { get; set; }
    }
}
