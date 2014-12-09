using System;
using ScriptCs.Contracts;

namespace ScriptingHost
{
    public interface IScriptConfig
    {
        string Name { get; set; }
        string[] Paths { get; set; }
        IXScriptPack[] ScriptPacks { get; set; }
        string[] ScriptParameters { get; set; }
        Type[] ReferenceAndImports { get; set; }
        string[] NameSpaces { get; set; }
    }

    /// <summary>
    /// Impostor ! 
    /// </summary>
    public interface IXScriptPack
    {
        void Initialize(IScriptPackSession session);

        IScriptPackContext GetContext();

        void Terminate();
    }
}