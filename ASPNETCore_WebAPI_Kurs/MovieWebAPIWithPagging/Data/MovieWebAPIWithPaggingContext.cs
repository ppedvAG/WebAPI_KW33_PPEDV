using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieWebAPIWithPagging.Models;

namespace MovieWebAPIWithPagging.Data
{
    public class MovieWebAPIWithPaggingContext : DbContext
    {
        public MovieWebAPIWithPaggingContext (DbContextOptions<MovieWebAPIWithPaggingContext> options)
            : base(options)
        {
        }

        public DbSet<MovieWebAPIWithPagging.Models.Movies> Movies { get; set; } = default!;
    }
}
