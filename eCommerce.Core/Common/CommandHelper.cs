using System.Diagnostics;

namespace eCommerce.Core.Common
{
    public class CommandHelper
    {
        public static string ExecuteCmd(string arguments)
        {
            var info =
                new ProcessStartInfo("cmd", @"/C " + arguments)
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

            var p = new Process {StartInfo = info};
            p.Start();

            // Capture the results in a string
            string processResults = p.StandardOutput.ReadToEnd();

            // Close the process to release system resources
            p.Close();

            // Return the output stream to the caller
            return processResults;
        }
    }
}
