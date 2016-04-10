using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Thompson_Trevor_HW7.Models;

namespace Thompson_Trevor_HW7.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class AppUser : IdentityUser
    {
     
        //TODO: Put any additional fields that you need for your user here
        //For instance
        public string FName { get; set; }
        public string LName { get; set; }
        public string UserID { get; set; }
        

        //This method allows you to create a new user
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    //TODO: Here's your db context for the project.  All of your db sets should go in here
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        //TODO:  Add dbsets here, for instance there's one for books
        //Remember, Identity adds a db set for users, so you shouldn't add that one - you will get an error
        public DbSet<Member> Members { get; set; }
        public DbSet<Committee> Committees { get; set; }
        public DbSet<Event> Events { get; set; }
                
        //TODO: Make sure that your connection string name is correct here.
        public AppDbContext()
            : base("ThompsonTrevorHW7", throwIfV1Schema: false)
        {
        }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }

        public System.Data.Entity.DbSet<Thompson_Trevor_HW7.Models.AppRole> AppRoles { get; set; }
    }
}