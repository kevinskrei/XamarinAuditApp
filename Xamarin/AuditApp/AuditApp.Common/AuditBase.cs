using System;

namespace AuditApp.Common
{
	public abstract class AuditBase
	{
		private const String KEY_IGNORE_VERSION = "com.auditmyapp.ignoreversion";
		private const String KEY_DONT_REMIND = "com.auditmyapp.versionsdontremind";
		private const String KEY_DATE_INSTALLED_VERSION = "com.auditmyapp.dateinstalled";
		private const String KEY_NUM_USES_FOR_VERSION = "com.auditmyapp.versionuses";
		private const String KEY_LAST_VERSION = "com.auditmyapp.lastversion";
		private const String KEY_REMIND_LATER = "com.auditmyapp.remindlater";
		private const String KEY_REMIND_LATER_DATE = "com.auditmyapp.remindlaterdate";

		/// <summary>
		/// The amount of time from when the user installs the app (or version) until the prompt will be shown.
		/// DEFAULT: 10 days
		/// 
		/// NOTE: This is used in conjunction with 'UsesUntilPrompt' to determine whether prompt should be shown. If you want
		/// the number of days to be ignored, set it to 0 seconds.
		/// </summary>
		/// <value>The time until prompt.</value>
		public TimeSpan TimeUntilPrompt { get; set; }

		/// <summary>
		/// The number of uses of the application before the prompt will be shown. 
		/// DEFAULT: 15 uses (15 Application OnCreate() calls)
		/// 
		/// NOTE: This is used in conjunction with 'TimeUntilPrompt' to determine whether the prompt should be shown. If you want
		/// the number of uses to be ignored, set it to '0'
		/// </summary>
		/// <value>The uses until prompt.</value>
		public int UsesUntilPrompt { get; set; }

		/// <summary>
		/// DEFAULT: TRUE
		/// 
		/// Set this to 'True' if you want to show the user a "Rating" prompt for this version and each future version. If this value is
		/// true, it will show a prompt after NumberOfUses and TimeUntilPrompt conditions are met for each version of your application.
		/// 
		/// Set this to 'False' if you want to only show one prompt for the lifetime of your application. With a new version, you can always set
		/// this back to 'True' and it will then show a prompt for all subsequent versions.
		/// </summary>
		/// <value><c>true</c> if ignore app versions; otherwise, <c>false</c>.</value>
		public bool IgnoreAppVersions { get; set; }

		/// <summary>
		/// The amount of time to wait after the user says 'Remind me Later'.
		/// 
		/// DEFAULT: 10 days
		/// </summary>
		/// <value>The remind later time to wait.</value>
		public TimeSpan RemindLaterTimeToWait { get; set; }

		/// <summary>
		/// Initial prompt title after days and uses have expired.
		/// 
		/// DEFAULT (PlaystoreAudit): "Could you take a minute to review this app?"
		/// DEFAULT (InterceptAudit): How is your experience with this app?"
		/// </summary>
		/// <value>The prompt title.</value>
		public string PromptTitle { get; set; }

		/// <summary>
		/// Text for Remind me later button on initial prompt
		/// 
		/// DEFAULT: "Remind me later"
		/// </summary>
		/// <value>The remind later button text.</value>
		public string RemindLaterButtonText { get; set; }

		/// <summary>
		/// Text for the Dont remind button on initial prompt
		/// 
		/// DEFAULT: "Don't remind me"
		/// </summary>
		/// <value>The dont remind button text.</value>
		public string DontRemindButtonText { get; set; }

		/// <summary>
		/// Occurs when app store is not found
		/// </summary>
        public event EventHandler AppStoreNotFound;

		private int _numUses;
		private DateTime _dateInstalledVersion;

		protected IAppStorage _appStorage;

		protected AuditBase(IAppStorage appStorage)
		{
			_appStorage = appStorage;
			InitDefaults ();
		}

		private void InitDefaults()
		{
			RemindLaterTimeToWait = new TimeSpan (10, 0, 0, 0);

			UsesUntilPrompt = 15;
			TimeUntilPrompt = new TimeSpan (10, 0, 0, 0);

			PromptTitle = "Could you take a minute to review this app?";
			RemindLaterButtonText = "Remind me later";
			DontRemindButtonText = "Don't remind me";
			IgnoreAppVersions = true;

			Init ();
		}

		protected void Localize(string promptTitle, string remindLaterButtonText, string dontRemindButtonText)
		{
			PromptTitle = promptTitle;
			RemindLaterButtonText = remindLaterButtonText;
			DontRemindButtonText = dontRemindButtonText;
		}

		protected void Init()
		{
            var lastVersion = GetVersion();
            var currentVersion = _appStorage.GetCurrentVersion();

            if (lastVersion != currentVersion && IgnoreAppVersions)
            {
                //Clear app settings 
                _appStorage.Clear();
            }

            SaveVersion(currentVersion);
            InitDateInstalled();
            IncrementUsageCount();
		}

		protected void RemindUserLater()
		{
			_appStorage.SaveBool (KEY_REMIND_LATER, true);
			_appStorage.SaveDate (KEY_REMIND_LATER_DATE, DateTime.Now.Add (RemindLaterTimeToWait));
		}

        protected void RaiseAppStoreNotFound()
        {
            if (AppStoreNotFound != null)
                AppStoreNotFound.Invoke(this, EventArgs.Empty);
        }

		protected void DontRemind()
		{
			_appStorage.SaveBool (KEY_DONT_REMIND, true);
		}

        private void SaveVersion(int currentVersion)
        {
            _appStorage.SaveInt(KEY_LAST_VERSION, currentVersion);
        }

        private int GetVersion()
        {
            return _appStorage.GetInt(KEY_LAST_VERSION, -1);
        }

		private void InitDateInstalled()
		{
			DateTime? dateInstalled = _appStorage.GetDate (KEY_DATE_INSTALLED_VERSION, null);
			if (!dateInstalled.HasValue) 
			{
				_dateInstalledVersion = DateTime.Now;
				_appStorage.SaveDate (KEY_DATE_INSTALLED_VERSION, _dateInstalledVersion);
			} 
			else 
			{
				_dateInstalledVersion = dateInstalled.Value;
			}
		}

		private bool IsRemindLater()
		{
			return _appStorage.GetBool (KEY_REMIND_LATER, false);
		}

		private bool IsPassedRemindLaterDate()
		{
			DateTime? dateToRemind = _appStorage.GetDate (KEY_REMIND_LATER_DATE, null);
			if (dateToRemind.HasValue) 
			{
				return dateToRemind < DateTime.Now;
			}
			return false;
		}

		private bool ShouldShowRemindLater()
		{
			return IsRemindLater () && IsPassedRemindLaterDate ();
		}

		private bool IsDontRemind()
		{
			return _appStorage.GetBool (KEY_DONT_REMIND, false);
		}

		private void IncrementUsageCount()
		{
			_numUses = _appStorage.GetInt (KEY_NUM_USES_FOR_VERSION, 0);
			_numUses++;
			_appStorage.SaveInt (KEY_NUM_USES_FOR_VERSION, _numUses);
		}

		private bool IsAfterDaysToWait()
		{
			return DateTime.Now > _dateInstalledVersion.Add (TimeUntilPrompt);
		}

		protected bool ShouldShowPrompt()
		{
			if (_numUses >= UsesUntilPrompt && IsAfterDaysToWait()) 
			{
				if (IsDontRemind () ||
					( IsRemindLater() && !IsPassedRemindLaterDate()) ) 
				{
					return false;
				}

				return true;
			}

			return false;
		}
	}
}

