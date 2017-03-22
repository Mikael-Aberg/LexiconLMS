using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LexiconLMS.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Du måste fylla i ett förnamn.")]
        [RegularExpression(@"^[a-zA-ZåäöÅÄÖ]{1}[a-zA-ZåäöÅÄÖ\-\s*]*$", ErrorMessage = "Mata in endast bokstäver!")]
        [StringLength(30, ErrorMessage = "Förnamnet får max vara 30 tecken långt.")]
        [DisplayName("Förnamn")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Du måste fylla i ett efternamn.")]
        [RegularExpression(@"^[a-zA-ZåäöÅÄÖ]{1}[a-zA-ZåäöÅÄÖ\-\s*]*$", ErrorMessage = "Mata in endast bokstäver!")]
        [StringLength(30, ErrorMessage = "Efternamnet får max vara 30 tecken långt.")]
        [DisplayName("Efternamn")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Du måste fylla i ett personnummer.")]
        //[RegularExpression(@"^(?:19|[2-9][0-9]){0,1}(?:[0-9]{2})(?!0229|0230|0231|0431|0631|0931|1131)
        //    (?:(?:0[1-9])|(?:1[0-2]))(?:(?:0[1-9])|(?:1[0-9])|(?:2[0-9])|(?:3[01]))[-+](?!0000)(?:[0-9]{4})$", 
        //    ErrorMessage = "Felaktigt format på personnummer!")]
        [DisplayName("Personnummer")]
        public string SocialSecurityNumber { get; set; }

        public int? CourseId { get; set; }

        [DisplayName("Namn")]
        public string FullName { get { return FirstName + " " + LastName; } }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public virtual Course Course { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
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

        public DbSet<Course> Courses { get; set; }
        
        public DbSet<Activity> Activities { get; set; }

        public DbSet<ActivityType> ActivityTypes { get; set; }

        public DbSet<Module> Modules { get; set; }

        public DbSet<Document> Documents { get; set; }
    }
}