using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using AuditApp.Common;
using AuditApp.Android;

namespace PlaystoreAudit
{
	[Activity(MainLauncher = true)]
	public class Activity1 : Activity//, IReviewAppPlaystoreCallback
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button>(Resource.Id.myButton);
			button.Text = "Open prompt manually";
			button.Click += delegate { 
				AndroidPlaystoreAudit.Instance.ShowPrompt(this);
			};
				
			//Uncomment the next line, the 2 methods below, and the interface in the class declaration to show a prompt before the user goes to the app store.
			//AndroidPlaystoreAudit.Instance.OnReviewAppPlaystore = this;

			AndroidPlaystoreAudit.Instance.AppStoreNotFound += (sender, e) => {
				Toast.MakeText(this, "Cannot find appstore", ToastLength.Long).Show();
			};
			AndroidPlaystoreAudit.Instance.Run(this);

		}

		#region IReviewAppPlaystoreCallback implementation

//		public bool OnReviewAppPlaystoreResponse ()
//		{
//			ShowProceedToAppStorePrompt ("Please review our app in the store", new string[] { "Proceed to Store" }, result => {
//				AndroidAuditUtils.OpenAppStore(this);
//			});
//			return false;
//		}
//
//
//		private void ShowProceedToAppStorePrompt(string title, string[] buttons, Action<int> selectedCallback)
//		{
//			AlertDialog.Builder builder = new AlertDialog.Builder(this);
//			builder.SetTitle(title);
//			builder.SetItems(buttons, (object sender, DialogClickEventArgs e) =>
//				{
//					if (selectedCallback != null)
//						selectedCallback(e.Which);
//				});
//
//			builder.Create();
//			builder.Show();
//		}
		#endregion
	}
}


