using System;
namespace ToDoManager.ClientShared.LocalData.Sync
{
	/// <summary>
	/// Implemented by every entity that is synced between the Client and the backend DB
	/// </summary>
	public interface ISyncable
	{
		SyncOperation Operation
		{
			get;
			set;
		}

		int SyncRound
		{
			get;
			set;
		}
	}


}
