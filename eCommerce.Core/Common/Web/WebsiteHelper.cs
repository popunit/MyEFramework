using System;
using System.IO;
using System.Web;
using System.Web.Hosting;

namespace eCommerce.Core.Common.Web
{
    public static class WebsiteHelper
    {
        private static AspNetHostingPermissionLevel? _currentTrustLevel;

        /// <summary>
        /// Get current website trust level
        /// </summary>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/1064274/get-current-asp-net-trust-level-programmatically
        /// </remarks>
        public static AspNetHostingPermissionLevel GetCurrentTrustLevel()
        {
            if (_currentTrustLevel.HasValue)
                return _currentTrustLevel.Value;
            foreach (AspNetHostingPermissionLevel trustLevel in
                new AspNetHostingPermissionLevel[]
                {
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

                    _currentTrustLevel = trustLevel;
                    return _currentTrustLevel.Value;
                }

            _currentTrustLevel = AspNetHostingPermissionLevel.None;
            return _currentTrustLevel.Value;
        }

        /// <summary>
        /// Map Path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string MapPath(string path)
        {
            // if path does not exist, return empty
            if (string.IsNullOrWhiteSpace(path))
                return string.Empty;

            // check if the website is hosted or not
            if (HostingEnvironment.IsHosted)
            {
                // The IsPathRooted method returns true if the first character is a directory separator character such as "\", 
                // or if the path starts with a drive letter and colon (:)
                if (!Path.IsPathRooted(path))
                {
                    return HostingEnvironment.MapPath(path);
                }
                else
                {
                    // relative path
                    if(path.StartsWith("\\"))
                        return HostingEnvironment.MapPath(path);
                    return path;
                }
            }
            else
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory; // get application current running directory
                string relativePath = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');
                return Path.Combine(baseDirectory, relativePath);
            }
        }
    }
}
