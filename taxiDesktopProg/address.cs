namespace taxiDesktopProg
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("address")]
    public partial class address
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public address()
        {
            orders = new HashSet<order>();
            orders1 = new HashSet<order>();
        }

        [Key]
        public long id_address { get; set; }

        [Required]
        [StringLength(250)]
        public string city { get; set; }

        [Required]
        [StringLength(250)]
        public string street { get; set; }

        [Required]
        [StringLength(50)]
        public string house { get; set; }

        [Required]
        [StringLength(50)]
        public string enrance { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order> orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order> orders1 { get; set; }
    }
}
