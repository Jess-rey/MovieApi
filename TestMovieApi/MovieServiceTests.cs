using Application.Definitions;
using Data.Definitions;
using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Implementations;

namespace TestMovieApi
{
    public class MovieServiceTests
    {
      
        [Fact]
        public void GetMovieById_ReturnsMovie_WhenMovieExists()
        {
            // Esta prueba verifica que el método GetMovieById del servicio MovieService
            // devuelve correctamente una instancia de Movie cuando el repositorio contiene
            // una película con el ID especificado.

            // Arrange: Se configura un mock del repositorio para devolver una película simulada.
            var mockRepo = new Mock<IMovieRepository>();
            var expectedMovie = new Movie { Id = 1, Film = "Matrix" };

            mockRepo.Setup(r => r.GetMovieById(It.IsAny<int>())).Returns(expectedMovie);
            var service = new MovieService(mockRepo.Object);

            // Act: Se llama al método del servicio con el ID 1.
            var result = service.GetMovieById(1);

            // Assert: Se verifica que el resultado no sea nulo y que los datos coincidan con lo esperado.// Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Matrix", result.Film);
        }
        [Fact]
        public void GetMoviesOrderedById_ReturnsOrderedList_WhenCalledWithValidParameters()
        {
            // Esta prueba verifica que el método GetMoviesOrderedById del servicio MovieService
            // devuelve una lista ordenada de películas según el orden especificado (ascendente o descendente)
            // y limitada al número total solicitado.

            // Arrange: Se configura una lista simulada de películas ordenadas ascendentemente.
            var mockRepo = new Mock<IMovieRepository>();
            var movies = new List<Movie>
            {
                new Movie { Id = 1, Film = "Matrix" },
                new Movie { Id = 2, Film = "Inception" },
                new Movie { Id = 3, Film = "Interstellar" }
            };

            mockRepo.Setup(r => r.GetMoviesOrderedById(2, "asc"))
                    .Returns(movies.OrderBy(m => m.Id).Take(2).ToList());

            var service = new MovieService(mockRepo.Object);

            // Act: Se llama al método del servicio con total = 2 y orden = "asc".
            var result = service.GetMoviesOrderedById(2, "asc");

            // Assert: Se verifica que se devuelvan 2 películas en orden ascendente.
            Assert.Equal(2, result.Count);
            Assert.Equal(1, result[0].Id);
            Assert.Equal(2, result[1].Id);
        }
        [Fact]
        public void Create_CallsRepositoryWithCorrectMovie()
        {
            // Arrange: Se crea un mock del repositorio y una instancia del servicio.
            var mockRepo = new Mock<IMovieRepository>();
            var service = new MovieService(mockRepo.Object);

            var newMovie = new Movie { Id = 10, Film = "Inception" };

            // Act: Se llama al método Create del servicio.
            service.Create(newMovie);

            // Assert: Se verifica que el método Create del repositorio fue llamado una vez con el objeto correcto.
            mockRepo.Verify(r => r.Create(It.Is<Movie>(m => m.Id == 10 && m.Film == "Inception")), Times.Once);
        }


    }
}
