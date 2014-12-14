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
using AuditApp.Android;

namespace InterceptCustomLocalized
{
	[Application]
	public class GlobalApplication : Application
	{
		public GlobalApplication(IntPtr handle, JniHandleOwnership transfer)
			: base(handle, transfer)
		{
		}

		public override void OnCreate()
		{
			base.OnCreate ();

			AndroidInterceptAudit.Instance.UsesUntilPrompt = 1;
			AndroidInterceptAudit.Instance.TimeUntilPrompt = new TimeSpan (0, 0, 0);
			AndroidInterceptAudit.Instance.ShowUserIdentifierTextfield = false;

			//
			//SEE Resources/Values/Styles.xml for the styles associated with the popup
			//

			//Localize method all string for this type of prompt. French here...
			AuditApp.Android.AndroidInterceptAudit.Instance.Localize ("Comment est votre expérience avec cette application?",
				"Rappelez-moi plus tard", "Ne pas me rappeler", "J'aime cette application", "Pas à la hauteur de mes attentes", "Comment voulez-vous que nous améliorer votre expérience? S'il vous plaît fournir vos commentaires ci-dessous.",
				string.Empty, "Placez vos commentaires ici", "S'il vous plaît fournir une valeur valide pour tous les domaines", "soumettre");
		}
	}
}

