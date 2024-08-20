using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> option): base(option) 
        { 
            
        }
        public DbSet<Domain.Property> Properties => Set<Domain.Property>();
        public DbSet<Image> images => Set<Image>();
    }
}

