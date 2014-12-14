This application is similar to iRate on the component store. Ask users to review your application after they have used it for awhile.

## Types of prompts ##

1. A prompt that will take the user to the app store, remind them later, or don't remind
2. A prompt that will ask the user how the app experience is. If they have a positive experience it will take them 
to the app store. If they have a negative experience it will open a suggestion prompt where they can enter comments about the experience.

All of the default behavior is customizable and
you can specify the number of uses and/or the number of days to wait to display the prompt.

This library uses the package name to find your app on the store. So the play store will show a blank page until your app is on the store.

You must follow 2 steps.

Put the setup code in your class that extends Application and in the OnCreate(..) override.

    using AuditApp.Common
    ...
    //This is in your Application class
    public override void OnCreate(Bundle bundle)
    {
    	AndroidPlaystoreAudit.Instance.UsesUntilPrompt = 5;
    	AndroidPlaystoreAudit.Instance.TimeUntilPrompt = new TimeSpan(0, 1, 0);
    }

Next, Put the `Run(Activity activity)` method in the activity where you want to show the user the prompt.

    using AuditApp.Common
    ...
    //This is in your activity
    public override void OnCreate(Bundle bundle)
    {
    	AndroidPlaystoreAudit.Instance.Run(this);
    }

The other class `AndroidInterceptAudit` is shown in the samples and 'Getting Started' if you want users to give you feedback outside
of the app store and inside your application.

## Manually Show Prompt & Open App Store##
You can also manually show the prompt whenever you'd like by calling
`ShowPrompt(Activity activity)`

You can also open your application page on the Google Play Store by calling
`AndroidAuditUtils.OpenAppStore(Activity activity)` 

## Localization ##
The default language is English but there is a convenient `Localize(...)` method to provide your own strings or translations.

See the samples and the 'Getting Started' for the other many types of configuration you can perform.

* Xamarin.iOS version coming soon

screenshots taken with placeit.net

<h2> Release Notes </h2>
<h3> Version 1.0 </h3>
Initial Release