using System.Diagnostics;

namespace eCommerce.Core.Common
{
    public class CommandHelper
    {
        public static string ExecuteCmd(string arguments)
        {
            ProcessStartInfo _info =
                new ProcessStartInfo("cmd", @"/C " + arguments);

            _info.RedirectStandardOutput = true;

            _info.UseShellExecute = false;

            _info.CreateNoWindow = true;

            Process p = new Process();
            p.StartInfo = _info;
            p.Start();

            // Capture the results in a string
            string _processResults = p.StandardOutput.ReadToEnd();

            // Close the process to release system resources
            p.Close();

            // Return the output stream to the caller
            return _processResults;
        }
    }
}
