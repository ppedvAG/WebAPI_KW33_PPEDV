using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FormattersSampleWebAPI.Models;

namespace FormattersSampleWebAPI.Data
{
    public class FootballClubDb : DbContext
    {
        public FootballClubDb (DbContextOptions<FootballClubDb> options)
            : base(options)
        {
        }

        public DbSet<FormattersSampleWebAPI.Models.FootballClub> FootballClub { get; set; } = default!;
        public DbSet<FormattersSampleWebAPI.Models.Contact> Contacts { get; set; } = default!;
    }
}
