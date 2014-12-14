using System;

namespace AuditApp.Common
{
	/// <summary>
	/// Interface that can be implemented if you want to intercept the 'Positive Experience' button press.
	/// You can show your user a message saying something like 'You will now be taken to the app store, please review our app'
	/// </summary>
	public interface IPositiveResponseInterceptCallback
	{
		/// <summary>
		/// Return 'True' if you want to proceed to the app store without interuption such as you showing another prompt.
		/// 
		/// Return 'False' if you want to stop the process and show the user a prompt before proceeding
		/// NOTE: You can manually call the static method OpenAppStore(...) after showing user your own prompt.
		/// <returns><c>true</c> to avoid interuption with flow; otherwise, <c>false</c>.</returns>
		/// </summary>
		bool OnPositiveUserResponse();
	}
}

