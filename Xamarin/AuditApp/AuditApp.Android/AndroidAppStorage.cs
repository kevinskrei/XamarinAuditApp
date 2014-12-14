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
using Android.Content.PM;

namespace AuditApp.Android
{
	internal class AndroidAppStorage : IAppStorage
	{
        private const String PREFS_FILE = "com.auditmyapp.preferences";
		#region implemented abstract members of AppStorageBase
		public void SaveBool (string key, bool value)
		{
			using(var editor = GetPreferences().Edit())
            {
                editor.PutBoolean(key, value);
                editor.Commit();
            }
		}
		public bool GetBool (string key, bool defaultValue)
		{
            return GetPreferences().GetBoolean(key, defaultValue);
		}
		public void SaveDate (string key, DateTime date)
		{
			using(var editor = GetPreferences().Edit())
            {
                editor.PutLong(key, date.Ticks);
                editor.Commit();
            }
		}
		public DateTime? GetDate (string key, DateTime? defaultValue)
		{
            var ticks = GetPreferences().GetLong(key, -1);
            return ticks < 0 ? defaultValue : new DateTime(ticks);
		}
		public void SaveString (string key, string value)
		{
			using(var editor = GetPreferences().Edit())
            {
                editor.PutString(key, value);
                editor.Commit();
            }
		}
		public string GetString (string key, string defaultValue)
		{
            return GetPreferences().GetString(key, defaultValue);
		}
		public void SaveInt (string key, int value)
		{
			using(var editor = GetPreferences().Edit())
            {
                editor.PutInt(key, value);
                editor.Commit();
            }
		}
		public int GetInt (string key, int defaultValue)
		{
            return GetPreferences().GetInt(key, defaultValue);
		}
		public void Clear ()
		{
			using(var editor = GetPreferences().Edit())
            {
                editor.Clear();
                editor.Commit();
            }
		}
		public int GetCurrentVersion ()
		{
            PackageInfo info = Application.Context.PackageManager.GetPackageInfo(Application.Context.PackageName, PackageInfoFlags.Activities);
            return info.VersionCode;
		}
		#endregion

        private ISharedPreferences GetPreferences()
        {
            return Application.Context.GetSharedPreferences(PREFS_FILE, FileCreationMode.Private);
        }
	}
}

