using System;

namespace AuditApp.Common
{
	/// <summary>
	/// Interface that can be implemented if you want to intercept the 'Review App' button press.
	/// You can show your user a message saying something like 'You will now be taken to the app store, please review our app'
	/// </summary>
	/// <value>The on review app playstore.</value>
	public interface IReviewAppPlaystoreCallback
	{
		/// <summary>
		/// Return 'True' if you want to proceed to the app store without interuption such as you showing another prompt.
		/// 
		/// Return 'False' if you want to stop the process and show the user a prompt before proceeding
		/// NOTE: You can manually call the static method OpenAppStore(...) after showing user your own prompt.
		/// <returns><c>true</c> to avoid interuption with flow; otherwise, <c>false</c>.</returns>
		/// </summary>
		bool OnReviewAppPlaystoreResponse();
	}
}

