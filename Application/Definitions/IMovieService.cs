using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Definitions
{
    public  interface IMovieService
    {
        Movie GetMovieById(int id);
        List<Movie> GetMoviesOrderedById(int total, string order);
        void Create(Movie obj);
    }
}
