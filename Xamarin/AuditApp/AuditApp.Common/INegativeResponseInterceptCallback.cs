using System;

namespace AuditApp.Common
{
	/// <summary>
	/// Interface that can be implemented if you want to intercept the 'Negative Experience' button press.
	/// You can show your own form or simply log this call.
	/// </summary>
	public interface INegativeResponseInterceptCallback
	{
		/// <summary>
		/// Return 'True' if you want to proceed to the suggestion prompt without interuption.
		/// 
		/// Return 'False' if you want to stop the process and show the user a prompt before proceeding
		/// NOTE: You can manually call the method ShowSuggestionPrompt(...) after performing your own logic.
		/// <returns><c>true</c> to avoid interuption with flow; otherwise, <c>false</c>.</returns>
		/// </summary>
		bool OnNegativeUserResponse();
	}
}

