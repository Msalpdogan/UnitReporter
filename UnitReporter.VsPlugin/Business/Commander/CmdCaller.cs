using System.Diagnostics;
using UnitTestReporter.Business.Interfaces;

namespace UnitTestReporter.Business.Classes
{
    public class CmdCaller : ICmdCaller
    {
        public CmdCaller()
        {
        }
        public string commandCreator()
        {
            return "TestCommand";
        }

        public string runCommand(string exe, string args)
        {
            try
            {
                var info = new ProcessStartInfo(exe);
                info.Arguments = args;
                info.RedirectStandardOutput = true;
                info.WindowStyle = ProcessWindowStyle.Hidden;

                var output = "";
                using (var process = Process.Start(info))
                {
                    output = process.StandardOutput.ReadToEnd();
                }
                return output.Trim();

            }
            catch (System.Exception ex)
            {
                Logger.Log(ex.ToString() + " commandCreator failed.");
                return "";
            }
        }
    }
}
