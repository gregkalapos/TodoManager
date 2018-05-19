using System;
using ToDoManager.ClientShared.Tools;
using Xunit;

namespace ClientSharedUnitTests
{
	public class MessagingCenterTests
	{
		static String MSGNAME = "MyMsg";

		static int INTPROPVAL = 42;
		static string STRINGPOPVAL = "Expected string value";

		[Fact]
		public void SendReceiveMessage()
		{
			SubscriberClass subscriber = new SubscriberClass();
			SenderClass sender = new SenderClass();

			Assert.Equal(INTPROPVAL, subscriber.IntVal);
			Assert.Equal(STRINGPOPVAL, subscriber.StringVal);
		}

		internal class Message
		{
			public String StrProp { get; set; }
			public int IntProp { get; set; }
		}

		internal class SenderClass
		{
			public SenderClass()
			=> MessagingCenter.Send(this, MSGNAME, new Message
			{
				IntProp = INTPROPVAL,
				StrProp = STRINGPOPVAL
			});
		}

		internal class SubscriberClass
		{
			public String StringVal { get; set; }
			public int IntVal { get; set; }
			public SubscriberClass() =>
				MessagingCenter.Subscribe<SenderClass, Message>(MSGNAME, (msg)=>{
					var mymsg = msg as Message;
					StringVal = mymsg.StrProp;
					IntVal = mymsg.IntProp;
				});
		}
	}
}
