using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Data.Definitions
{
    public interface IMovieRepository : IGenericRepository<Movie>
    {

        Movie GetMovieById(int? id);

        List<Movie> GetMoviesOrderedById(int total, string order);

    }

}
