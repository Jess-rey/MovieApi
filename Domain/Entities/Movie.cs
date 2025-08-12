using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Movie
    {
        public int Id { get; set; }                     // Identificador único
        public string Film { get; set; } = string.Empty; // Nombre de la película
        public string Genre { get; set; } = string.Empty; // Género
        public string Studio { get; set; } = string.Empty; // Estudio productor
        public int Score { get; set; }                   // Puntuación (0 - 100)
        public int Year { get; set; }                    // Año de estreno
    }
}
