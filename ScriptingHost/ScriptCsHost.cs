using System;
using System.Linq;
using Common.Logging;
using ImpromptuInterface;
using ScriptCs;
using ScriptCs.Contracts;
using ScriptCs.Engine.Roslyn;
using ScriptCs.Hosting;
using LogLevel = ScriptCs.Contracts.LogLevel;
using XResult = System.Tuple<ScriptCs.Contracts.ScriptResult,System.Exception>;

namespace ScriptingHost
{
    public class ScriptCsHost
    {
        public ScriptServices Root { get; private set; }

        public ScriptCsHost()
        {
            var logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            var scriptServicesBuilder =
                new ScriptServicesBuilder(new ScriptConsole(), logger).LogLevel(LogLevel.Info)
                    .Cache(false)
                    .Repl(false)
                    .ScriptEngine<RoslynScriptEngine>();

            Root = scriptServicesBuilder.Build();          
        }


        public Func<string, XResult> Setup(IScriptConfig res)
        {
            if (res.NameSpaces != null && res.NameSpaces.Any()) Root.Executor.ImportNamespaces(res.NameSpaces);

            if (res.ScriptPacks != null)
                Root.Executor.Initialize(res.Paths, res.ScriptPacks.Select(x => x!=null ? x.ActLike<IScriptPack>() : null));

            if (res.ReferenceAndImports != null) 
                Root.Executor.AddReferenceAndImportNamespaces(res.ReferenceAndImports);

            return script =>
            {
                ScriptResult result1 = null;
               
                Exception ex = null;
                try
                {
                    result1 = Root.Executor.ExecuteScript(script, res.ScriptParameters);
                }
                catch (Exception e)
                {
                    ex = e;
                }
                finally
                {
                    Root.Executor.Terminate();
                }

                return new Tuple<ScriptResult, Exception>(result1,ex);
            };           
        }
    }
}