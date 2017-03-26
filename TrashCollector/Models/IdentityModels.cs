using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TrashCollector.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here

            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Zip> ZipCodes { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Month> Months { get; set; }
        public DbSet<Week> Weeks { get; set; }
        public DbSet<Time> Times { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<SetSchedule> SetSchedules { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<SetWeekSchedule> SetWeekSchedules { get; set; }
        public DbSet<Billing> Billings { get; set; }
        public DbSet<EmployeeRouter> EmployeeRouters { get; set; }
        public System.Data.Entity.DbSet<TrashCollector.Models.Account> Accounts { get; set; }
    }
}