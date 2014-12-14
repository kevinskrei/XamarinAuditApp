using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AuditApp.Android;

namespace PlaystoreAudit
{
	[Application]
	public class GlobalApplication : Application
	{
		public GlobalApplication(IntPtr handle, JniHandleOwnership transfer)
			: base(handle, transfer)
		{
		}

		public override void OnCreate()
		{
			base.OnCreate ();

			AndroidPlaystoreAudit.Instance.UsesUntilPrompt = 1;
			AndroidPlaystoreAudit.Instance.TimeUntilPrompt = new TimeSpan (0, 0, 0);
			//You don't need to do anything else here unless you want to.
			//If you hit 'Remind me later' you must exit the application and wait 15 seconds. Open the app and then you'll see the prompt.
			AndroidPlaystoreAudit.Instance.RemindLaterButtonText = "Remind me in 15 seconds";
			AndroidPlaystoreAudit.Instance.RemindLaterTimeToWait = new TimeSpan (0, 0, 15);
		}
	}
}

