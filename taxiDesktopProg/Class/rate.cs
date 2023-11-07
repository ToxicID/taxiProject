namespace taxiDesktopProg
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("rate")]
    public partial class rate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public rate()
        {
            orders = new HashSet<order>();
        }

        [Key]
        public long id_rate { get; set; }

        public bool availability { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        public decimal boarding { get; set; }

        public decimal cost_per_kilometer { get; set; }

        public decimal cost_downtime { get; set; }

        public decimal? child_safety_seat { get; set; }

        public decimal? transportation_of_pet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order> orders { get; set; }
    }
}
