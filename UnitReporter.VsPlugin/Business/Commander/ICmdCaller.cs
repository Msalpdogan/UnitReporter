
namespace UnitTestReporter.Business.Interfaces
{
    public interface ICmdCaller
    {
        string runCommand(string exe, string args);
        string commandCreator();
    }
}
