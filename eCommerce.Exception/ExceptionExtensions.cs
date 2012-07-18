using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Exception
{
    /// <summary>
    /// Utility methods for exceptions.
    /// </summary>
    public static class ExceptionExtensions
    {
        private static readonly FieldInfo remoteStackTraceString =
            typeof(System.Exception).GetField("_remoteStackTraceString", BindingFlags.Instance | BindingFlags.NonPublic) ??
            typeof(System.Exception).GetField("remote_stack_trace", BindingFlags.Instance | BindingFlags.NonPublic);

        /// <summary>
        /// Rethrows an exception object without losing the existing stack trace information.
        /// </summary>
        /// <param name="ex">The exception to re-throw.</param>
        /// <remarks>
        /// For more information on this technique, see
        /// http://www.dotnetjunkies.com/WebLog/chris.taylor/archive/2004/03/03/8353.aspx
        /// </remarks>
        public static void RethrowWithNoStackTraceLoss(this System.Exception ex)
        {
            remoteStackTraceString.SetValue(ex, ex.StackTrace + Environment.NewLine);

            throw ex;
        }
    }
}
