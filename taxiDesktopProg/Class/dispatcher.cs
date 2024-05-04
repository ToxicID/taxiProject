namespace taxiDesktopProg
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("dispatcher")]
    public partial class dispatcher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public dispatcher()
        {
            orders = new HashSet<order>();
        }

        [Key]
        public long id_dispatcher { get; set; }

        public bool activity { get; set; }

        [Required]
        [StringLength(150)]
        public string surname { get; set; }

        [Required]
        [StringLength(150)]
        public string name { get; set; }

        [StringLength(150)]
        public string patronymic { get; set; }

        [Required]
        [StringLength(11)]
        public string mobile_phone { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order> orders { get; set; }
    }
}
