namespace taxiDesktopProg
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class work_schedule
    {
        [Key]
        public long id_work_schedule { get; set; }

        [Column(TypeName = "date")]
        public DateTime date_of_work_from { get; set; }

        public TimeSpan work_schedule_from { get; set; }

        public TimeSpan? work_schedule_before { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date_of_work_before { get; set; }

        public long id_driver { get; set; }

        public virtual driver driver { get; set; }
    }
}
