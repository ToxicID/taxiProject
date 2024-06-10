using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace taxiDesktopProg
{
    public partial class Context : DbContext
    {
        public Context(string connectionString)
            : base(connectionString)
        {
        }

        public virtual DbSet<address> addresses { get; set; }
        public virtual DbSet<car> cars { get; set; }
        public virtual DbSet<car_category> car_category { get; set; }
        public virtual DbSet<client> clients { get; set; }
        public virtual DbSet<dispatcher> dispatchers { get; set; }
        public virtual DbSet<driver> drivers { get; set; }
        public virtual DbSet<order> orders { get; set; }
        public virtual DbSet<order_history> order_history { get; set; }
        public virtual DbSet<rate> rates { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<violation> violations { get; set; }
        public virtual DbSet<work_schedule> work_schedule { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<address>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<address>()
                .Property(e => e.street)
                .IsUnicode(false);

            modelBuilder.Entity<address>()
                .Property(e => e.house)
                .IsUnicode(false);

            modelBuilder.Entity<address>()
                .Property(e => e.enrance)
                .IsUnicode(false);

            modelBuilder.Entity<address>()
                .HasMany(e => e.orders)
                .WithRequired(e => e.address)
                .HasForeignKey(e => e.destination)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<address>()
                .HasMany(e => e.orders1)
                .WithRequired(e => e.address1)
                .HasForeignKey(e => e.place_of_departure)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<car>()
                .Property(e => e.colour)
                .IsUnicode(false);

            modelBuilder.Entity<car>()
                .Property(e => e.car_brand)
                .IsUnicode(false);

            modelBuilder.Entity<car>()
                .Property(e => e.car_model)
                .IsUnicode(false);

            modelBuilder.Entity<car>()
                .Property(e => e.state_number)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<car>()
                .Property(e => e.region_code)
                .IsUnicode(false);

            modelBuilder.Entity<car>()
                .Property(e => e.technical_condition_car)
                .IsUnicode(false);

            modelBuilder.Entity<car_category>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<car_category>()
                .Property(e => e.fuel_consumption)
                .HasPrecision(5, 2);

            modelBuilder.Entity<client>()
                .Property(e => e.mobile_phone)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<dispatcher>()
                .Property(e => e.surname)
                .IsUnicode(false);

            modelBuilder.Entity<dispatcher>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<dispatcher>()
                .Property(e => e.patronymic)
                .IsUnicode(false);

            modelBuilder.Entity<dispatcher>()
                .Property(e => e.mobile_phone)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<driver>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<driver>()
                .Property(e => e.driver_readiness)
                .IsUnicode(false);

            modelBuilder.Entity<driver>()
                .Property(e => e.call_sign)
                .IsUnicode(false);

            modelBuilder.Entity<driver>()
                .Property(e => e.surname)
                .IsUnicode(false);

            modelBuilder.Entity<driver>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<driver>()
                .Property(e => e.patronymic)
                .IsUnicode(false);

            modelBuilder.Entity<driver>()
                .Property(e => e.drivers_license_number)
                .IsUnicode(false);

            modelBuilder.Entity<driver>()
                .Property(e => e.mobile_phone)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<driver>()
                .HasMany(e => e.orders)
                .WithOptional(e => e.driver)
                .WillCascadeOnDelete();

            modelBuilder.Entity<order>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<order>()
                .Property(e => e.reason_cancellation)
                .IsUnicode(false);

            modelBuilder.Entity<order>()
                .Property(e => e.order_cost)
                .HasPrecision(6, 2);

            modelBuilder.Entity<order>()
                .Property(e => e.payment_method)
                .IsUnicode(false);

            modelBuilder.Entity<order_history>()
                .Property(e => e.call_sign)
                .IsUnicode(false);

            modelBuilder.Entity<order_history>()
                .Property(e => e.surname)
                .IsUnicode(false);

            modelBuilder.Entity<order_history>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<order_history>()
                .Property(e => e.patronymic)
                .IsUnicode(false);

            modelBuilder.Entity<order_history>()
                .Property(e => e.mobile_phone)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<order_history>()
                .Property(e => e.colour)
                .IsUnicode(false);

            modelBuilder.Entity<order_history>()
                .Property(e => e.car_brand)
                .IsUnicode(false);

            modelBuilder.Entity<order_history>()
                .Property(e => e.car_model)
                .IsUnicode(false);

            modelBuilder.Entity<order_history>()
                .Property(e => e.state_number)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<order_history>()
                .Property(e => e.region_code)
                .IsUnicode(false);

            modelBuilder.Entity<order_history>()
                .Property(e => e.place_of_departure)
                .IsUnicode(false);

            modelBuilder.Entity<order_history>()
                .Property(e => e.destination)
                .IsUnicode(false);

            modelBuilder.Entity<order_history>()
                .Property(e => e.client_mobile_phone)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<rate>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<rate>()
                .Property(e => e.boarding)
                .HasPrecision(6, 2);

            modelBuilder.Entity<rate>()
                .Property(e => e.cost_per_kilometer)
                .HasPrecision(6, 2);

            modelBuilder.Entity<rate>()
                .Property(e => e.cost_downtime)
                .HasPrecision(6, 2);

            modelBuilder.Entity<rate>()
                .Property(e => e.child_safety_seat)
                .HasPrecision(6, 2);

            modelBuilder.Entity<rate>()
                .Property(e => e.transportation_of_pet)
                .HasPrecision(6, 2);

            modelBuilder.Entity<violation>()
                .Property(e => e.type_of_violation)
                .IsUnicode(false);

            modelBuilder.Entity<violation>()
                .Property(e => e.violation_status)
                .IsUnicode(false);

            modelBuilder.Entity<violation>()
                .Property(e => e.measures_taken)
                .IsUnicode(false);

            modelBuilder.Entity<work_schedule>()
                .Property(e => e.work_schedule_from)
                .HasPrecision(0);

            modelBuilder.Entity<work_schedule>()
                .Property(e => e.work_schedule_before)
                .HasPrecision(0);
        }
    }
}
