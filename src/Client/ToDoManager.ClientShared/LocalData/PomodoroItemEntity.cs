using System;
using System.Collections.Generic;
using System.Text;
using ToDoManager.ClientShared.LocalData.Sync;
using ToDoManager.Model;

namespace ToDoManager.ClientShared.LocalData
{
	public class PomodoroItemEntity : PomodoroItemModel, ISyncable
	{
		public SyncOperation Operation { get; set; }
		public int SyncRound { get; set; }
	}
}
