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
using AuditApp.Common;

namespace AuditApp.Android
{
	public class AndroidInterceptAudit : InterceptAuditBase, IAuditPrompt
    {
        private static readonly Lazy<AndroidInterceptAudit> lazy = new Lazy<AndroidInterceptAudit>(() => new AndroidInterceptAudit());
        public static AndroidInterceptAudit Instance { get { return lazy.Value; } }

		private AndroidInterceptAudit() : base(new AndroidAppStorage()) {}

		/// <summary>
		/// Runs the checks (UsesUntilPrompt and TimeUntilPrompt or remind me later) to determine 
		/// whether to show the prompt or not. Call this from any activity you want to prompt the user. 
		/// 
		/// This does NOT update the count of uses. The Init function increments the app count.
		/// </summary>
		/// <param name="activity">Activity.</param>
        public void Run(Activity activity)
        {
			if(ShouldShowPrompt())
            {
                Show(activity);
            }
        }

		/// <summary>
		/// Manually show the rating prompt. You can use this if you want to manually show the prompt
		/// without constraints.
		/// </summary>
		/// <param name="activity">Activity.</param>
        public void ShowPrompt(Activity activity)
        {
            Show(activity);
        }

        private void Show(Activity activity)
        {
            AndroidAuditUtils.ShowPrompt(activity, PromptTitle, new string[] {
				PositiveButtonText,
                NegativeButtonText,
				RemindLaterButtonText,
				DontRemindButtonText
			},
            result =>
            {
                if (result == 0)
                {
					DontRemind();
                    if (OnPositiveResponseCallback == null || 
                        (OnPositiveResponseCallback != null && OnPositiveResponseCallback.OnPositiveUserResponse()))
                        AndroidAuditUtils.OpenAppStore(activity);
                }
                else if(result == 1)
                {
					DontRemind();
                    if (OnNegativeResponseCallback == null || 
                        (OnNegativeResponseCallback != null && OnNegativeResponseCallback.OnNegativeUserResponse()))
                        OpenSuggestionPrompt(activity);
                }
                else if (result == 2)
                {
                    RemindUserLater();
                }
                else if (result == 3)
                {
                    DontRemind();
                }
            });
        }

        private void OpenSuggestionPrompt(Activity activity)
        {
            View view = activity.LayoutInflater.Inflate(Resource.Layout.intercept_suggestion_layout, null);
			Dialog dialog = new Dialog (activity);
			dialog.RequestWindowFeature ((int)WindowFeatures.NoTitle);

			view.FindViewById<TextView> (Resource.Id.intercept_suggestion_header).Text = SuggestionHeaderText;
			dialog.SetContentView (view);
			dialog.Window.SetLayout (ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.WrapContent);

            var userid = view.FindViewById<EditText>(Resource.Id.intercept_suggestion_id);

            if(!ShowUserIdentifierTextfield)
            {
                userid.Visibility = ViewStates.Gone;
            }
            else
            {
                userid.Hint = UserIdentifierHint;
            }

            var suggestionEdit = view.FindViewById<EditText>(Resource.Id.intercept_suggestion_comment);
            suggestionEdit.SetLines(6);
            suggestionEdit.Hint = UserCommentsHint;

			var submitButton = view.FindViewById<Button> (Resource.Id.intercept_suggestion_done);
			submitButton.Text = SubmitButtonText;
			submitButton.Click += delegate {
				if(!IsUserIdValid(userid.Text) || suggestionEdit.Text.Length <= 0)
                {
                    Toast.MakeText(activity, UserCommentValidationText, ToastLength.Long).Show();
                }
                else
                {
                    RaiseOnUserResponse(userid.Text, suggestionEdit.Text);
                    dialog.Dismiss();
                }
            };

			dialog.Show ();
        }
    }
}