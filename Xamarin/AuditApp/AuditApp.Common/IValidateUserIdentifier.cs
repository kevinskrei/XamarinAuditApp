using System;

namespace AuditApp.Common
{
	public interface IValidateUserIdentifier
	{
		/// <summary>
		/// Determines whether user id provided is valid.
		/// </summary>
		/// <returns><c>true</c> if the user identifier is valid; otherwise, <c>false</c>.</returns>
		/// <param name="userId">User identifier.</param>
		bool IsUserIdValid(string userId);
	}
}

