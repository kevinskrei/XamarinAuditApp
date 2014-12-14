using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using AuditApp.Android;
using AuditApp.Common;

namespace InterceptCustomLocalized
{
	[Activity (Label = "InterceptCustomLocalized", MainLauncher = true)]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			Button button = FindViewById<Button> (Resource.Id.myButton);
			button.Text = "Open prompt manually";
			button.Click += delegate {
				//You can manually show the prompt by calling ShowPrompt(...);
				AuditApp.Android.AndroidInterceptAudit.Instance.ShowPrompt(this);
			};

			AndroidInterceptAudit.Instance.UserResponded += (object sender, UserSuggestionEventArgs e) => {
				//Send to web server or save to file
				Toast.MakeText(this, e.UserResponse, ToastLength.Long).Show();
			};

			AuditApp.Android.AndroidInterceptAudit.Instance.Run (this);
		}
	}
}


