using System;
namespace ToDoManager.ClientShared.LocalData.Sync
{
	/// <summary>
	/// Stores sync information from the local database
	/// </summary>
	public class SyncInfo
	{
		public int SyncRound
		{
			get;
			set;
		}
	}
}