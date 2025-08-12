using Data.Contexts;
using Data.Definitions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Data.Implementations
{
    /// <summary>
    /// Repositorio para acceder a datos de películas.
    /// </summary>
    internal class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        private readonly IContext _context;

        /// <summary>
        /// Inicializa una nueva instancia del repositorio de películas.
        /// </summary>
        /// <param name="context">Contexto de base de datos inyectado.</param>
        public MovieRepository(IContext context) : base(context)
        {
            _context = context;
        }

        //public List<Movie> ListMovie()
        //{
        //    return _context.Movies.ToList();
        //}
        /// <summary>
        /// Busca una película por su identificador único.
        /// </summary>
        /// <param name="id">Identificador de la película.</param>
        /// <returns>Objeto <see cref="Movie"/> si se encuentra; de lo contrario, <c>null</c>.</returns>
        public Movie GetMovieById(int? id)
        {
            if (!id.HasValue)
                return null;

            return _context.Movies.FirstOrDefault(m => m.Id == id.Value);
        }
        /// <summary>
        /// Retrieves a list of movies ordered by their ID in ascending or descending order.
        /// </summary>
        /// <param name="total">The maximum number of movies to return.</param>
        /// <param name="order">
        /// The sort direction for the movie IDs. Accepts "asc" for ascending or "desc" for descending.
        /// Case-insensitive.
        /// </param>
        /// <returns>
        /// A list of movies ordered by ID according to the specified direction,
        /// limited to the specified total count.
        /// </returns>
        public List<Movie> GetMoviesOrderedById(int total, string order)
        {
            var query = _context.Movies.AsQueryable();

            query = order.ToLower() == "desc"
                ? query.OrderByDescending(m => m.Id)
                : query.OrderBy(m => m.Id);

            return query.Take(total).ToList();
        }



        /// <summary>
        /// Busca una película por su identificador único.
        /// </summary>
        /// <param name="id">Identificador de la película.</param>
        /// <returns>Objeto <see cref="Movie"/> si se encuentra; de lo contrario, <c>null</c>.</returns>
        public class DesignTimeContextFactory : IDesignTimeDbContextFactory<Context>
        {
            /// <summary>
            /// Crea una instancia del contexto usando SQLite.
            /// </summary>
            /// <param name="args">Argumentos de línea de comandos (no usados).</param>
            /// <returns>Instancia de <see cref="Context"/>.</returns>
            public Context CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<Context>();
                optionsBuilder.UseSqlite("Data Source=prueba.db");

                return new Context(optionsBuilder.Options);
            }
        }
    }

}
