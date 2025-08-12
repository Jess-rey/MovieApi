using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Domain.Entities;
using Data.Mapping;

namespace Data.Contexts
{
    public class Context : DbContext, IContext
    {
    
        public DbSet<Movie> Movies { get; set; }

    
        public Task SaveChangesAsync() => base.SaveChangesAsync();

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.ApplyConfiguration(new MovieMapping());
        }

        public Context(DbContextOptions<Context> options) : base(options) { }
    }

}
