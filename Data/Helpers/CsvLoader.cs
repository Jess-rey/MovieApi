using Data.Contexts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Helpers
{
    public class CsvLoader : ICsvLoader
    {
        private readonly IContext _context;

        public CsvLoader(IContext context)
        {
            _context = context;
        }

        public async Task LoadMoviesAsync(string filePath)
        {
            if (!File.Exists(filePath)) return;

            var lines = await File.ReadAllLinesAsync(filePath);
            if (lines.Length <= 1) return;

            var movies = new List<Movie>();

            foreach (var line in lines.Skip(1))
            {
                var parts = line.Split(',');

                if (parts.Length < 6 || string.IsNullOrWhiteSpace(parts[0])) continue;

                try
                {
                    var movie = new Movie
                    {
                        Id = int.Parse(parts[0]),
                        Film = parts[1],
                        Genre = parts[2],
                        Studio = parts[3],
                        Score = int.Parse(parts[4]),
                        Year = int.Parse(parts[5])
                    };

                    movies.Add(movie);
                }
                catch
                {
                    continue;
                }
            }

            if (!await _context.Movies.AnyAsync())
            {
                _context.Movies.AddRange(movies);
                await _context.SaveChangesAsync();
            }
        }
    }

}
