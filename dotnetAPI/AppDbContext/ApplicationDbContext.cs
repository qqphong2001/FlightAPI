using dotnetAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace dotnetAPI.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                //var tableName = entityType.GetTableName();
                //if (tableName.StartsWith("AspNet"))
                //{
                //    entityType.SetTableName(tableName.Substring(6));
                //}

            }
        }
        public DbSet<User> Users { get; set; }
        public DbSet<AirPort> airPorts { get; set; }

        public DbSet<Flight> Flights { get; set; }

        public DbSet<FlightRoute> FlightsRoute { get; set; }

        public DbSet<Price> Prices { get; set; }

        public DbSet<Status> Statuss { get; set; }

        public DbSet<AirLine> AirLines { get; set; }

        public DbSet<FlightRouteDetail> FlightRouteDetail { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<Ticket> Ticket { get; set; }

        public DbSet<TicketClass> TicketClass { get; set; }

        public DbSet<tempCustomer> TempCustomer { get; set; }

        public DbSet<nationCCID> nationCCID { get; set;}

        public DbSet<Invoice> Invoice { get; set; }

        public DbSet<BookingTicket> BookingTicket { get; set; }

    }
}
