using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace eCommerce.Core.Common.Web
{
    internal static class WebsiteHelper
    {
        private static AspNetHostingPermissionLevel? currentTrustLevel;

        /// <summary>
        /// Get current website trust level
        /// </summary>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/1064274/get-current-asp-net-trust-level-programmatically
        /// </remarks>
        internal static AspNetHostingPermissionLevel GetCurrentTrustLevel()
        {
            if (currentTrustLevel.HasValue)
                return currentTrustLevel.Value;
            foreach (AspNetHostingPermissionLevel trustLevel in
                    new AspNetHostingPermissionLevel[] {
            AspNetHostingPermissionLevel.Unrestricted,
            AspNetHostingPermissionLevel.High,
            AspNetHostingPermissionLevel.Medium,
            AspNetHostingPermissionLevel.Low,
            AspNetHostingPermissionLevel.Minimal 
        })
            {
                try
                {
                    new AspNetHostingPermission(trustLevel).Demand();
                }
                catch (System.Security.SecurityException)
                {
                    continue;
                }

                currentTrustLevel =  trustLevel;
                return currentTrustLevel.Value;
            }

            currentTrustLevel = AspNetHostingPermissionLevel.None;
            return currentTrustLevel.Value;
        }
    }
}
