using System;
using System.Linq;
using System.Threading.Tasks;
using ScriptCs.Contracts;

namespace ScriptingHost
{
    public class ScriptRunner
    {
        private readonly IScriptProvider _scriptProvider;
        
        private readonly ScriptCsHost _host;

        private readonly IScriptConfigManager _scriptConfigManager;

        public ScriptRunner(IScriptProvider scriptProvider, ScriptCsHost host,IScriptConfigManager scriptConfigManager)
        {
            _scriptConfigManager = scriptConfigManager;
            _scriptProvider = scriptProvider;
            _host = host;
        }

        public async Task<object> Execute(IScriptInfo scriptInfo)
        {           

            var scriptContent= await _scriptProvider.GetScriptContent(scriptInfo);

            try
            {
                var executor = _host.Setup(_scriptConfigManager.GetConfig(scriptInfo));
                var result = executor(scriptContent).OnError(e => { throw e; }).OnSuccess(rr => AsResult(rr));
                return result.Item1;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        private object AsResult(ScriptResult scriptResult)
        {
            var exceptions = new[]
            {
                scriptResult.CompileExceptionInfo,
                scriptResult.ExecuteExceptionInfo
            }
                .Select(e => e != null ? e.SourceException : null)
                .Select(error => error != null ? error.Message : null)
                .ToArray();

            return exceptions.Any(xx => xx != null)
                 ? exceptions.Aggregate((a, b) => a + "\n" + b)
                 : scriptResult.ReturnValue != null ? scriptResult.ReturnValue.ToString() : null;
        }
    }
}