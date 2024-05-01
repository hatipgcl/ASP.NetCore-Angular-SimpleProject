using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;
using System;
using Microsoft.EntityFrameworkCore;
using CarAngularProject.Entity;

namespace CarAngularProject
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext([NotNull] DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}

	
		public DbSet<Car> Cars { get; set; }
	}
}
