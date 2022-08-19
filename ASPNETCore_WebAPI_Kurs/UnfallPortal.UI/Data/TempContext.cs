using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UnfallPortal.Shared.Entities;

namespace UnfallPortal.UI.Data
{
    public class TempContext : DbContext
    {
        public TempContext (DbContextOptions<TempContext> options)
            : base(options)
        {
        }

        public DbSet<UnfallPortal.Shared.Entities.Mandant> Mandant { get; set; } = default!;

        public DbSet<UnfallPortal.Shared.Entities.MandantUnfall>? MandantUnfall { get; set; }

        public DbSet<UnfallPortal.Shared.Entities.MandantUnfallDokumente>? MandantUnfallDokumente { get; set; }

        public DbSet<UnfallPortal.Shared.Entities.ErsteHilfeKurs>? ErsteHilfeKurs { get; set; }

        public DbSet<UnfallPortal.Shared.Entities.MandantKurs>? MandantKurs { get; set; }
    }
}
