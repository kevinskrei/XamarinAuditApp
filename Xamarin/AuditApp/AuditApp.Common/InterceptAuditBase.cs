using System;

namespace AuditApp.Common
{
	public abstract class InterceptAuditBase : AuditBase
	{
		/// <summary>
		/// Text for second prompt after user has clicked the 'Negative' experience button on the initial prompt.
		/// 
		/// DEFAULT: "How is your experience with this app?"
		/// </summary>
		/// <value>The suggestion header text.</value>
		public string SuggestionHeaderText { get; set; }

		/// <summary>
		/// Text for inital prompt indicating user has had a positive experience with the app and will take them to the app store
		/// 
		/// DEFAULT: "I like this app"
		/// </summary>
		/// <value>The positive button text.</value>
		public string PositiveButtonText { get; set; }

		/// <summary>
		/// Text for inital prompt indicating user has had a negative experience with the app and will prompt them for their comments/suggestions
		/// 
		/// DEFAULT: "Not up to my expectations"
		/// </summary>
		/// <value>The negative button text.</value>
		public string NegativeButtonText { get; set; }

		/// <summary>
		/// Used in the second prompt whether or not to show the user identifier text field. Use this if you want to contact your users further
		/// and follow up with their negative experience. 
		/// 
		/// NOTE: If you already know which user is using the app, set this to false as to not make your user enter information you already have.
		/// </summary>
		/// <value><c>true</c> if show user identifier textfield; otherwise, <c>false</c>.</value>
		public bool ShowUserIdentifierTextfield { get; set; }

		/// <summary>
		/// Used in the second prompt as a hint (placeholder) for the user identifier field. If 'ShowUserIdentifierTextfield' is false, this string won't be shown.
		/// 
		/// DEFAULT: "Enter your email address"
		/// </summary>
		/// <value>The user identifier hint.</value>
		public string UserIdentifierHint { get; set; }

		/// <summary>
		/// Used in the second prompt as the multi-line edit text hint (placeholder) to allow user to enter comments about negative experience.
		/// 
		/// DEFAULT: "Place your comments here"
		/// </summary>
		/// <value>The user comments hint.</value>
		public string UserCommentsHint { get; set; }

		/// <summary>
		/// Used in the second prompt. Supply a string here if you want to validate the user identifier or/and the suggestion text field. For example, if you need the user id
		/// to be an email, you can provide a different message here.
		/// 
		/// DEFAULT: "Please provide a valid value for all fields"
		/// </summary>
		/// <value>The user comment validation text.</value>
		public string UserCommentValidationText { get; set; }

		/// <summary>
		/// Used in second prompt. Submit button text after user has entered comments about negative experience.
		/// 
		/// DEFAULT: "Submit"
		/// </summary>
		/// <value>The submit button text.</value>
		public string SubmitButtonText { get; set; }

		/// <summary>
		/// Implement this interface and set it here if you want to intercept and show the user another prompt before
		/// continuing to the app store. Or prevent them from going to the appstore. See 'IPostiveResponseInterceptCallback' interface.
		/// </summary>
		public IPositiveResponseInterceptCallback OnPositiveResponseCallback { get; set; }

		/// <summary>
		/// Implement this interface and set it here if you want to intercept and show the user another prompt before
		/// showing the suggestion prompt (or perform your own logic). See 'INegativeResponseInterceptCallback' interface.
		/// </summary>
		public INegativeResponseInterceptCallback OnNegativeResponseCallback { get; set; }

		/// <summary>
		/// Implement this interface and set it here if you want to validate the User Identifier text field before submission.
		/// If you need to make sure the user id is an email address (or some other requirement), you must implement this interface
		/// </summary>
		public IValidateUserIdentifier OnValidateUserId { get; set; }

		/// <summary>
		/// Event fires when user hits submit button after entering comments/ user id (if applicable) on the second (suggestions) prompt.
		/// </summary>
		public event EventHandler<UserSuggestionEventArgs> UserResponded;

		protected InterceptAuditBase(IAppStorage appStorage) : base(appStorage)
		{
			ShowUserIdentifierTextfield = true;
			Localize ("How is your experience with this app?", RemindLaterButtonText, DontRemindButtonText,
				"I like this app", "Not up to my expectations", 
				"How would you like us to improve your experience? Please provide your comments below.",
				"Enter your email address", "Place your comments here", "Please provide a valid value for all fields", "Submit");
		}

		protected void RaiseOnUserResponse(string userId, string comment)
		{
			if (UserResponded != null)
				UserResponded.Invoke(this, new UserSuggestionEventArgs()
					{
						UserResponse = comment,
						UserId = userId
					});
		}

		protected bool IsUserIdValid(string text)
		{
			return OnValidateUserId == null || OnValidateUserId.IsUserIdValid(text);
		}

		public void Localize(string promptTitle, string remindLaterButtonText, string dontRemindButtonText, string positiveButtonText,
			string negativeButtonText, string suggestionHeaderText, string userIdentifierHint, string userCommentsHint, string userCommentsValidationText, string doneButtonText)
		{
			base.Localize (promptTitle, remindLaterButtonText, dontRemindButtonText);
			LocalizeSelf (positiveButtonText, negativeButtonText, suggestionHeaderText, userIdentifierHint, userCommentsHint, userCommentsValidationText, doneButtonText);
		}

		private void LocalizeSelf(string positiveButtonText,
			string negativeButtonText, string suggestionHeaderText, string userIdentifierHint, string userCommentsHint, string userCommentsValidationText, string doneButtonText)
		{
			PositiveButtonText = positiveButtonText;
			NegativeButtonText = negativeButtonText;
			SuggestionHeaderText = suggestionHeaderText;
			UserIdentifierHint = userIdentifierHint;
			UserCommentsHint = userCommentsHint;
			UserCommentValidationText = userCommentsValidationText;
			SubmitButtonText = doneButtonText;
		}
	}
}

