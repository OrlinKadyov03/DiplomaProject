using KadiovVehicleCare.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KadiovVehicleCare.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Client> Clients { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Appointment>()
                .HasOne(a => a.Client)
                .WithMany(c => c.Appointments)
                .HasForeignKey(a => a.ClientId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Appointment>()
                .HasOne(a => a.Car)
                .WithMany(c => c.Appointments)
                .HasForeignKey(a => a.CarId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Appointment>()
                .HasOne(a => a.Service)
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => a.ServiceId)
                .OnDelete(DeleteBehavior.NoAction);
        }


    }
}
