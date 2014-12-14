using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using AuditApp.Common;
using AuditApp.Android;

namespace InterceptAuditDefault
{
	[Activity (Label = "InterceptAuditDefault", MainLauncher = true)]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);

			button.Click += delegate {
				AuditApp.Android.AndroidInterceptAudit.Instance.ShowPrompt(this);
			};


			AndroidInterceptAudit.Instance.UserResponded += (object sender, UserSuggestionEventArgs e) => {
				//Send to web server or save to file
				Toast.MakeText(this, e.UserResponse, ToastLength.Long).Show();
			};
			AndroidInterceptAudit.Instance.Run (this);
		}
	}
}


