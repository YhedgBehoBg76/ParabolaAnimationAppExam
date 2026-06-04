using System;
using System.Threading.Tasks;

namespace ParabolaAnimationApp.Services
{
	public interface ILaunchDatabase
	{
		Task AddLaunchAsync(DateTime time);
	}
}
