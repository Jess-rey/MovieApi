using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contexts
{
    public interface IContext
    {
        public DbSet<Movie> Movies { get; set; }

        Task SaveChangesAsync();
    }

}

