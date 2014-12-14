using System;

namespace AuditApp.Common
{
	public interface IAppStorage
	{
		void SaveBool(string key, bool value);
		bool GetBool(string key, bool defaultValue);
		void SaveDate(string key, DateTime date);
		DateTime? GetDate(string key, DateTime? defaultValue);
		void SaveString (string key, string value);
		string GetString(string key, string defaultValue);
		void SaveInt (string key, int value);
		int GetInt (string key, int defaultValue);
		void Clear ();

		int GetCurrentVersion();
	}
}

