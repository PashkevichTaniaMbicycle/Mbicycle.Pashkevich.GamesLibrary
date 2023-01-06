﻿using GamesLib.DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace GamesLib.DataAccess.Context
{
    public sealed class GamesLibContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        
        public DbSet<Dev> Devs { get; set; }
        
        public DbSet<Publisher> Publishers { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Server=PC;Database=GamesLib;Trusted_Connection=True")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);;
        }
    }
}