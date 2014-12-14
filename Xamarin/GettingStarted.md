This will guide you through the two types of prompts you can show the user. Please check out the 
samples to see these as working examples.

## Playstore Audit Example ##
The first example is called a 'Playstore' audit. 
It will show a prompt with 3 options and if they select "Review App" it will take them to the app store.
You can configure the amount of time to wait and usages before the prompt is displayed.

put all of the initialization code in the OnCreate of your class that extends Application. 
Put the `Run(this)` method in one activity that you want to show the prompt.

    using AuditApp.Common;
    ...
    //In your class that extends Application. Please see the default values in the intellisense for each property on the class.
    public override void OnCreate()
    {
    	//You'll have to open, kill the app, then open it again to see the prompt since we are setting 'UsesUntilPrompt' = 2
    	AndroidPlaystoreAudit.Instance.TimeUntilPrompt = new TimeSpan(0, 0, 10);
    	AndroidPlaystoreAudit.Instance.UsesUntilPrompt = 2;
    	AndroidPlaystoreAudit.Instance.RemindLaterButtonText = "Remind me in 30 seconds";
		AndroidPlaystoreAudit.Instance.RemindLaterTimeToWait = new TimeSpan (0, 0, 30);
    }

Now put this in your activity where you want to show the prompt.

    //In your Activity
    public override void OnCreate(Bundle bundle)
    {
    	AndroidPlaystoreAudit.Instance.AppStoreNotFound += (sender, e) => {
			Toast.MakeText(this, "Cannot find appstore", ToastLength.Long).Show();
		};
    	AndroidPlaystoreAudit.Instance.Run(this);
    }

##  Intercept Audit Example  ##
The next example is called an 'Intercept' audit. It will show a prompt with 4 buttons. One button
will be for a positive experience and will direct them to the app store. The negative experience button
will open another prompt where the user can enter suggestions for improvement. The last two buttons are for
remind me later and don't remind.

    using AuditApp.Common;
	...
	
	//In your class that extends Application.
	public override void OnCreate()
	{
		//You can also hide the User Identifier text field if you already know who the user is by
		//Setting the property 'ShowUserIdentifier' to 'false'
		AuditApp.Android.AndroidInterceptAudit.Instance.UserIdentifierHint = "Enter User Id";
		AuditApp.Android.AndroidInterceptAudit.Instance.UsesUntilPrompt = 1;
		AuditApp.Android.AndroidInterceptAudit.Instance.TimeUntilPrompt = new TimeSpan (0, 0, 0);
		AuditApp.Android.AndroidInterceptAudit.Instance.RemindLaterTimeToWait = new TimeSpan (0, 0, 10);
	}

In your activity where you want to show the prompt.

    //In your activity that you want to show the prompt.
	public override void OnCreate (Bundle bundle)
	{
		AuditApp.Android.AndroidInterceptAudit.Instance.UserResponded += (object sender, UserSuggestionEventArgs e) => {
			//Send to web server or save to file
			Toast.MakeText(this, e.UserResponse, ToastLength.Long).Show();
		};
		Android.AndroidInterceptAudit.Instance.Run (this);
	}

There are many more ways to customize the experience such as localization and settings styles for the prompts. You can also return bool's on whether to perform the default action when the user responds.
See the InterceptCustomAndLocalized project in the samples or the documentation on how to perform these functions.

Make sure to put all of your initialization code and event handler hook-ups before calling


    ...Instance.Run(Activity activity);


## Manually Show Prompt & Open App Store##
You can also manually show the prompt whenever you'd like by calling
`ShowPrompt(Activity activity)`

You can also open your application page on the Google Play Store by calling
`AndroidAuditUtils.OpenAppStore(Activity activity)` 

## Localization ##
The default language is English but there is a convenient `Localize(...)` method to provide your own strings or translations.

See the samples and the 'Getting Started' for the other many types of configuration you can perform.

## Review Each Version ##
You can ask the user to review each version of your application by setting `...Instance.IgnoreAppVersion = true`. This basically resets any responses by the user when they download a new version of the application and the counts are reset.

**Note: The default IgnoreAppVersion is false and will only prompt once for the lifetime of the application**

