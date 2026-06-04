using System;
using Microsoft.EntityFrameworkCore;
using ParabolaAnimationApp.Models;

namespace ParabolaAnimationApp.Services
{
	public class AppDbContext : DbContext
	{
		public DbSet<LaunchRecord> Launches { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Data Source=launches.db");
        }
	}
}
