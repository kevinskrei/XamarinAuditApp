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
using AuditApp.Common;

namespace InterceptAuditDefault
{
	[Application]
	public class GlobalApplication : Application, IValidateUserIdentifier
	{
		public GlobalApplication(IntPtr handle, JniHandleOwnership transfer)
			: base(handle, transfer)
		{
		}

		public override void OnCreate()
		{
			base.OnCreate ();

			AndroidInterceptAudit.Instance.UsesUntilPrompt = 1;
			AndroidInterceptAudit.Instance.TimeUntilPrompt = new TimeSpan (0, 0, 0);
			AndroidInterceptAudit.Instance.UserIdentifierHint = "Enter User Id";
			AndroidInterceptAudit.Instance.UserCommentValidationText = "Please provide a user id of length 5 and a comment";
			//If you remind later, you must exit (and kill) the application, wait 10 seconds and open it again to see the prompt
			AndroidInterceptAudit.Instance.RemindLaterTimeToWait = new TimeSpan (0, 0, 10);
			//You can remove this and the interface if you don't want to validate.
			AndroidInterceptAudit.Instance.OnValidateUserId = this;
		}

		#region IValidateUserIdentifier implementation

		public bool IsUserIdValid (string userId)
		{
			return userId.Length == 5;
		}

		#endregion
	}
}

