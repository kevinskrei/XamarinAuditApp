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
	internal interface IAuditPrompt
    {
        void Run(Activity activity);
        void ShowPrompt(Activity activity);
    }
}