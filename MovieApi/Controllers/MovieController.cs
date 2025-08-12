using Application.Definitions;
using Data.Response;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace MovieApi.Controllers
{

    [Route("api")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _service;
        private readonly ILogger _logger;

        public MovieController(IMovieService service, ILogger<MovieController> logger)
        {
            _service = service;
            _logger = logger;
        }


        [HttpGet("movie")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            try
            {
                var movie =  _service.GetMovieById(id);

                if (movie == null)
                    return NotFound(new ApiResponse<string>(404, "Pelicula NO encontrada", ""));

                return Ok(new ApiResponse<Movie>(200, "Pelicula encontrada", movie));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Se ha producido un error al recuperar la película.");
                return StatusCode(500, new ApiResponse<string>(500, ex.Message, ""));
            }
        }
        [HttpGet("movies")]
        public IActionResult GetMoviesOrderedById([FromQuery] int total, [FromQuery] string order)
        {
            try
            {
                // Validación del parámetro 'order'
                var normalizedOrder = order?.ToLower();
                if (normalizedOrder != "asc" && normalizedOrder != "desc")
                {
                    return BadRequest(new ApiResponse<string>(
                        400,
                        "El parámetro 'order' debe ser 'asc' o 'desc'.",
                        ""));
                }

                // Llamada al servicio
                var movies = _service.GetMoviesOrderedById(total, normalizedOrder);

                if (movies == null || movies.Count == 0)
                {
                    return NotFound(new ApiResponse<string>(
                        404,
                        "No se encontraron películas.",
                        ""));
                }

                return Ok(new ApiResponse<List<Movie>>(
                    200,
                    "Películas obtenidas exitosamente.",
                    movies));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de películas ordenadas por ID.");
                return StatusCode(500, new ApiResponse<string>(
                    500,
                    "Ocurrió un error interno en el servidor.",
                    ""));
            }
        }
        [HttpPost("movie")]
        public async Task<IActionResult> CreateMovie([FromBody] Movie movieDto)
        {
            try
            {
                if (movieDto == null)
                {
                    return BadRequest(new ApiResponse<string>(
                        400,
                        "Los datos de la película son requeridos.",
                        ""));
                }

                 _service.Create(movieDto);

                return CreatedAtAction(nameof(GetById), new { id = movieDto.Id },
                    new ApiResponse<Movie>(
                        201,
                        "La película fue creada con éxito.",
                        movieDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la película.");
                return StatusCode(500, new ApiResponse<string>(
                    500,
                    "Ocurrió un error interno al crear la película.",
                    ""));
            }
        }


    }
}

