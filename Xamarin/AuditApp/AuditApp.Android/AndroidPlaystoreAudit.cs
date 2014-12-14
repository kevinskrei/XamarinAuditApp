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
	public class AndroidPlaystoreAudit : AuditApp.Common.PlaystoreAuditBase, IAuditPrompt
	{
        private static readonly Lazy<AndroidPlaystoreAudit> lazy = new Lazy<AndroidPlaystoreAudit>(() => new AndroidPlaystoreAudit());
        public static AndroidPlaystoreAudit Instance { get { return lazy.Value; } }
		private AndroidPlaystoreAudit() : base(new AndroidAppStorage()) {}

		/// <summary>
		/// Runs the checks (UsesUntilPrompt and TimeUntilPrompt or remind me later) to determine 
		/// whether to show the prompt or not. Call this from any activity you want to prompt the user. 
		/// 
		/// This does NOT update the count of uses. The Init function increments the app count.
		/// </summary>
		/// <param name="activity">Activity.</param>
		public void Run(Activity activity)
		{
			if (ShouldShowPrompt())
				Show (activity);
		}

		/// <summary>
		/// Manually show the rating prompt. You can use this if you want to manually show the prompt
		/// without constraints.
		/// </summary>
		/// <param name="activity">Activity.</param>
		public void ShowPrompt(Activity activity)
		{
			Show (activity);
		}

		private void Show(Activity activity)
		{
			AndroidAuditUtils.ShowPrompt (activity, PromptTitle, new string[] {
				ReviewAppStoreButtonText,
				RemindLaterButtonText,
				DontRemindButtonText
			}, 
			result => {
                if(result == 0)
                {
					DontRemind();
					if(OnReviewAppPlaystore == null || OnReviewAppPlaystore != null && OnReviewAppPlaystore.OnReviewAppPlaystoreResponse()) {
						AndroidAuditUtils.OpenAppStore(activity);
					} 
                }
                else if(result == 1)
                {
                    RemindUserLater();
                }
                else if(result == 2)
                {
                    DontRemind();
                }
			});
		}
	}
}

