namespace taxiDesktopProg
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class violation
    {
        [Key]
        public long id_violations { get; set; }

        public long id_driver { get; set; }

        public DateTime datetime_of_recording_violation { get; set; }

        public DateTime datetime_the_violation { get; set; }

        [Required]
        [StringLength(200)]
        public string type_of_violation { get; set; }

        [Required]
        [StringLength(100)]
        public string violation_status { get; set; }

        [StringLength(150)]
        public string measures_taken { get; set; }

        public virtual driver driver { get; set; }
    }
}
