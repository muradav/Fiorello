using FrontToBack.Models;
using Microsoft.EntityFrameworkCore;

namespace FrontToBack.DAL
{
    public class AppDbContext:DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {

        }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderContent> SliderContents { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<AboutImage> AboutImages { get; set; }
        public DbSet<AboutContent> AboutContents { get; set; }
        public DbSet<Expert> Experts { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Florist> Florists { get; set; }
        public DbSet<SocialAddressFlorist> SocialAddressFlorists { get; set; }

    }
}
