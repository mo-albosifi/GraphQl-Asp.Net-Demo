using DataBase.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQl_HotChocolate_Demo.DataBase
{
    public class GraphQlDemoDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }

        public GraphQlDemoDbContext(DbContextOptions<GraphQlDemoDbContext> options) : base(options)
        {
        }
    }

}