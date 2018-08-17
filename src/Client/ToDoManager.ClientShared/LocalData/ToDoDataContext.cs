using System;
using Microsoft.EntityFrameworkCore;
using ToDoManager.ClientShared.LocalData.Sync;

namespace ToDoManager.ClientShared.LocalData
{
	public class ToDoDataContext: DbContext
	{
		public DbSet<TodoItemEntity> Todos
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
