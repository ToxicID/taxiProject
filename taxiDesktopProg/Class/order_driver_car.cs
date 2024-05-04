namespace taxiDesktopProg
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class order_driver_car
    {
        [Key]
        public long id_order_driver_car { get; set; }

        public long id_driver { get; set; }

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

        [Required]
        [StringLength(11)]
        public string mobile_phone { get; set; }

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

        [Required]
        [StringLength(6)]
        public string state_number { get; set; }

        [Required]
        [StringLength(3)]
        public string region_code { get; set; }

        [Required]
        [StringLength(300)]
        public string place_of_departure { get; set; }

        [Required]
        [StringLength(300)]
        public string destination { get; set; }

        public DateTime datetime_placing_the_order { get; set; }

        public DateTime? order_completion_datetime { get; set; }

        [Required]
        [StringLength(11)]
        public string client_mobile_phone { get; set; }
    }
}
