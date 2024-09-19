using BlogaatAPI.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogaatAPI.Data
{
    public class ApplicationDbcontet : IdentityDbContext<IdentityUser>
    {
        // Default constructor
        public ApplicationDbcontet() { }

        // Constructor with options
        public ApplicationDbcontet(DbContextOptions<ApplicationDbcontet> options) : base(options) { }

        // Define DbSets for your entities
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }

        // OnConfiguring method, if you need to add extra configurations
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
