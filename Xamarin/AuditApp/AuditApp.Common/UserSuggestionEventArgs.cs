using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditApp.Common
{
	public class UserSuggestionEventArgs : EventArgs
    {
        public string UserResponse { get; set; }
        public string UserId { get; set; }
    }
}
