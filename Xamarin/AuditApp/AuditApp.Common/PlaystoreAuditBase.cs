using System;

namespace AuditApp.Common
{
	public abstract class PlaystoreAuditBase : AuditBase
	{
		/// <summary>
		/// Text for button that will send the user to the app store.
		/// 
		/// DEFAULT: "Review app"
		/// </summary>
		/// <value>The review app store button text.</value>
		public string ReviewAppStoreButtonText { get; set; }

		/// <summary>
		/// Implement this interface and set it here if you want to intercept and show the user another prompt before
		/// continuing to the app store. Or prevent them from going to the appstore. See 'IReviewAppPlaystoreCallback' interface.
		/// </summary>
		public IReviewAppPlaystoreCallback OnReviewAppPlaystore { get; set; }

		protected PlaystoreAuditBase(IAppStorage appStorage) : base(appStorage)
		{
			ReviewAppStoreButtonText = "Review app";
		}

		public void Localize(string promptTitle, string remindLaterButtonText, string dontRemindButtonText, string reviewAppStoreButtonText)
		{
			ReviewAppStoreButtonText = reviewAppStoreButtonText;
			base.Localize (promptTitle, remindLaterButtonText, dontRemindButtonText);
		}
	}
}

