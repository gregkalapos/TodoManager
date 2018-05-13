using System;
using Microsoft.EntityFrameworkCore;
using ToDoManager.Data.Entities;

namespace ToDoManager.Data
{
    public class ToDoDbContext : DbContext
    {
		//public ToDoDbContext()
		//{ }

		public ToDoDbContext(DbContextOptions<ToDoDbContext> options)
			: base(options)
		{

		}

		public DbSet<ToDoItem> ToDoItems { get; set; }
        public DbSet<PomodoroItem> PomodoroItems { get; set; }
    }
}