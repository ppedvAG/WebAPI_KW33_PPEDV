using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KonventionenSamplesWebAPI.Models;

namespace KonventionenSamplesWebAPI.Data
{
    public class KonventionenSamplesWebAPIContext : DbContext
    {
        public KonventionenSamplesWebAPIContext (DbContextOptions<KonventionenSamplesWebAPIContext> options)
            : base(options)
        {
        }

        public DbSet<KonventionenSamplesWebAPI.Models.Movie> Movie { get; set; } = default!;
    }
}
