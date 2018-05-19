using System;
using System.Collections.Generic;
using System.Linq;

namespace ToDoManager.ClientShared.Tools
{
	/// <summary>
	/// Offers decoupled messaging
	/// </summary>
	public static class MessagingCenter
	{
		private static Dictionary<(string messageName, Type senderType), Action<object>> subscribers
		= new Dictionary<(string messageName, Type senderType), Action<object>>();

		/// <summary>
		/// Retisters a subscriber
		/// </summary>
		/// <returns>The subscribe.</returns>
		/// <param name="messageName">Message name.</param>
		/// <param name="action">Action to execute</param>
		/// <typeparam name="Sender">The 1st type parameter.</typeparam>
		/// <typeparam name="Message">The 2nd type parameter.</typeparam>
		public static void Subscribe<Sender, Message>(string messageName, Action<object> action)
		{
			if(!subscribers.ContainsKey((messageName, typeof(Sender) )))
			{
				subscribers[(messageName, typeof(Sender))] = action;
			}
		}

		public static void Send<Message>(Object sender, string messageName, Message message)
		{
			var matchingSubscrubers = 
				subscribers.Keys.Where(n => n.senderType == sender.GetType() && n.messageName == messageName);

			foreach (var item in matchingSubscrubers)
			{
				subscribers[item].Invoke(message);
			}
		}
	}
}
