using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class ApplicationDBContext : IdentityDbContext<Users>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> option): base(option) 
        { 
            
        }
        public DbSet<Domain.Property> Properties => Set<Domain.Property>();
        public DbSet<Image> images => Set<Image>();
        public DbSet<Users> users => Set<Users>();
    }
}

