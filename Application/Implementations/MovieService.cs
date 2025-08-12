using Application.Definitions;
using Data.Definitions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implementations
{
    /// <summary>
    /// Servicio que encapsula la lógica de negocio relacionada con películas.
    /// </summary>
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movie;
        /// <summary>
        /// Inicializa una nueva instancia del servicio de películas.
        /// </summary>
        /// <param name="movie">Repositorio de películas inyectado.</param>
        public MovieService(IMovieRepository movie)
        {
            _movie = movie;
        }
        /// <summary>
        /// Obtiene una película por su identificador único.
        /// </summary>
        /// <param name="id">Identificador de la película.</param>
        /// <returns>Instancia de <see cref="Movie"/> si se encuentra; de lo contrario, <c>null</c>.</returns>
        public Movie GetMovieById(int id)
        {
            return _movie.GetMovieById(id);
        }
        /// <summary>
        /// Obtiene una lista de películas ordenadas por su identificador (ID).
        /// </summary>
        /// <param name="total">Cantidad máxima de películas a devolver.</param>
        /// <param name="order">
        /// Dirección del ordenamiento por ID. Puede ser <c>"asc"</c> para ascendente o <c>"desc"</c> para descendente.
        /// </param>
        /// <returns>Lista de instancias de <see cref="Movie"/> que cumplen con los criterios especificados.</returns>
        public List<Movie> GetMoviesOrderedById(int total, string order)
        {
            return _movie.GetMoviesOrderedById(total, order);
        }
        /// <summary>
        /// Crea una nueva película en el sistema.
        /// </summary>
        /// <param name="obj">Instancia de <see cref="Movie"/> que contiene los datos de la película a registrar.</param>
        public void Create(Movie obj)
        {
            _movie.Create(obj); 
        }


    }
}
