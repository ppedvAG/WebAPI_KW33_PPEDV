using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Overview_WEBAPI.Models;

namespace Overview_WEBAPI.Data
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext (DbContextOptions<MovieDbContext> options)
            : base(options)
        {
        }

        public DbSet<Overview_WEBAPI.Models.Movie> Movies { get; set; } = default!;
    }
}
