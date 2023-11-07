namespace taxiDesktopProg
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("order")]
    public partial class order
    {
        [Key]
        public long id_order { get; set; }

        [Required]
        [StringLength(100)]
        public string status { get; set; }

        public long place_of_departure { get; set; }

        public long destination { get; set; }

        public decimal order_cost { get; set; }

        [Required]
        [StringLength(100)]
        public string payment_method { get; set; }

        public DateTime datetime_placing_the_order { get; set; }

        public DateTime? order_completion_datetime { get; set; }

        public long? id_driver { get; set; }

        public long id_client { get; set; }

        public long id_dispatcher { get; set; }

        public long id_rate { get; set; }

        public virtual address address { get; set; }

        public virtual address address1 { get; set; }

        public virtual client client { get; set; }

        public virtual dispatcher dispatcher { get; set; }

        public virtual driver driver { get; set; }

        public virtual rate rate { get; set; }
    }
}
