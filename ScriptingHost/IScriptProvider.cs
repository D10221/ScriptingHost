using System.Threading.Tasks;

namespace ScriptingHost
{
    public interface IScriptProvider
    {
        Task<string> GetScriptContent(IScriptInfo scriptInfo);
    }
}