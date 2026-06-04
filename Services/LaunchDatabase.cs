using System;
using System.Threading.Tasks;
using ParabolaAnimationApp.Models;

namespace ParabolaAnimationApp.Services
{
	public class LaunchDatabase : ILaunchDatabase
	{
		private readonly AppDbContext _context;

		public LaunchDatabase(AppDbContext context)
		{
			_context = context;
			_context.Database.EnsureCreated();
		}

		public async Task AddLaunchAsync(DateTime time)
		{
			var record = new LaunchRecord { LaunchTime = time };
			_context.Launches.Add(record);
			await _context.SaveChangesAsync();
		}
	}
}
