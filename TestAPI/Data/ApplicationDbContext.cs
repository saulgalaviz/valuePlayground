using Microsoft.EntityFrameworkCore;
using TestAPI.Models;

namespace TestAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Value> Values { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //To add migration, do an add-migration SeedValueTable on PM
            //If values updates, do an update with context such as add-migration SeedValueTableWithCreatedDate when we added new CreatedDate value
            modelBuilder.Entity<Value>().HasData(
                new Value
                {
                    Id = 1,
                    Name = "Lagoon",
                    Details = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed lorem dolor, hendrerit ut justo sit amet, dictum facilisis magna. Nullam pulvinar tempus leo, sit amet dapibus nisi convallis et. Pellentesque sodales vestibulum quam quis laoreet. Aenean rhoncus est nec consectetur.",
                    ImageUrl = "https://whvn.cc/47y633",
                    Occupancy = 8,
                    Rate = 200,
                    Sqft = 850,
                    Amenity = "",
                    CreatedDate = DateTime.Now,
                },
                new Value
                {
                    Id = 2,
                    Name = "Park",
                    Details = "Phasellus tempor sapien vitae ullamcorper gravida. Aenean eget pulvinar risus. Cras molestie ligula nibh. Sed luctus condimentum leo, nec tristique tellus vulputate id. Duis semper lorem sed lacus iaculis, ac fringilla mi vestibulum. Cras vel nunc mollis, sagittis nunc ac.",
                    ImageUrl = "https://whvn.cc/47y633",
                    Occupancy = 4,
                    Rate = 600,
                    Sqft = 1500,
                    Amenity = "",
                    CreatedDate = DateTime.Now,
                },
                new Value
                {
                    Id = 3,
                    Name = "Vegas Room",
                    Details = "Nullam bibendum lorem sed augue accumsan tempus. Ut quis bibendum eros, at malesuada odio. Duis congue dolor tincidunt orci sodales, id sagittis ante ultrices. Vivamus dapibus magna vel mi malesuada varius. Morbi volutpat eu metus dictum pretium. Proin quis tellus.",
                    ImageUrl = "https://whvn.cc/47y633",
                    Occupancy = 12,
                    Rate = 2000,
                    Sqft = 600,
                    Amenity = "",
                    CreatedDate = DateTime.Now,
                },
                new Value
                {
                    Id = 4,
                    Name = "Maui Island Resort",
                    Details = "Morbi mattis dolor mattis, faucibus quam sit amet, vestibulum orci. Quisque congue purus non ligula condimentum, at iaculis tellus faucibus. Proin eu diam sit amet diam finibus feugiat et vitae nisi. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed.",
                    ImageUrl = "https://whvn.cc/47y633",
                    Occupancy = 2,
                    Rate = 1000,
                    Sqft = 400,
                    Amenity = "",
                    CreatedDate = DateTime.Now,
                },
                new Value
                {
                    Id = 5,
                    Name = "Detroit Condo",
                    Details = "In quis mattis erat. Mauris pellentesque diam eu volutpat sagittis. Mauris ullamcorper, elit id consectetur euismod, lacus mauris tincidunt massa, ac rhoncus risus lorem eget lectus. Aliquam erat volutpat. Maecenas efficitur, nulla nec volutpat congue, magna justo interdum nulla, at.",
                    ImageUrl = "https://whvn.cc/47y633",
                    Occupancy = 6,
                    Rate = 800,
                    Sqft = 800,
                    Amenity = "",
                    CreatedDate = DateTime.Now,
                }
                );

        }
    }
}
