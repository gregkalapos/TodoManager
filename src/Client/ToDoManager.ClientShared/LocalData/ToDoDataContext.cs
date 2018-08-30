using System;
using Microsoft.EntityFrameworkCore;
using ToDoManager.ClientShared.LocalData.Sync;
using AutoMapper;
using ToDoManager.Model;

namespace ToDoManager.ClientShared.LocalData
{
	public class ToDoDataContext: DbContext
	{
		static ToDoDataContext()
		{
			Mapper.Initialize(cfg => {
				cfg.CreateMap<ToDoItemModel, TodoItemEntity>();
				cfg.CreateMap<PomodoroItemModel, PomodoroItemEntity>();
			});
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Data Source=todomanager.db");
		}

		public DbSet<TodoItemEntity> Todos
		{
			get;
			set;
		}

		public DbSet<PomodoroItemEntity> PomodoroItems
		{
			get;
			set;
		}

		public DbSet<SyncInfo> SyncInfo
		{
			get;
			set;
		}
	}
}
