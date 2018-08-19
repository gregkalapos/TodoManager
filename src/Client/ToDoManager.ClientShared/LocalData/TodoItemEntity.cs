using System;
using ToDoManager.ClientShared.LocalData.Sync;
using ToDoManager.Model;

namespace ToDoManager.ClientShared.LocalData
{
	public class TodoItemEntity : ToDoItemModel, ISyncable
	{
		public SyncOperation Operation { get; set; }
		public int SyncRound { get; set; }
	}
}
