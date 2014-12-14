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

namespace AuditApp.Android
{
	public class AndroidAuditUtils
    {
		public static void OpenAppStore(Activity activity)
        {
            Intent intent = new Intent(Intent.ActionView);
            intent.SetData(global::Android.Net.Uri.Parse("market://details?id=" + Application.Context.PackageName));
			intent.SetFlags (ActivityFlags.NewTask);
            Application.Context.StartActivity(intent);
        }

		internal static void ShowPrompt(Activity activity, string title, string[] buttons, Action<int> selectedCallback)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(activity);
            builder.SetTitle(title);
            builder.SetItems(buttons, (object sender, DialogClickEventArgs e) =>
            {
                if (selectedCallback != null)
                    selectedCallback(e.Which);
            });

            builder.Create();
            builder.Show();
        }


    }
}