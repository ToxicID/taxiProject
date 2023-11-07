namespace taxiDesktopProg
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class car_category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public car_category()
        {
            cars = new HashSet<car>();
        }

        [Key]
        public long id_car_category { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        public int number_of_passengers { get; set; }

        public decimal fuel_consumption { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<car> cars { get; set; }
    }
}
