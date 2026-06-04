using System;
using System.ComponentModel.DataAnnotations;

namespace ParabolaAnimationApp.Models
{
	public class LaunchRecord
	{
		[Key]
		public int Id { get; set; }

		public DateTime LaunchTime { get; set; }
	}
}
