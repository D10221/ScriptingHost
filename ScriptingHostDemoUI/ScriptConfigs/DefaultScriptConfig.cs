using System;
using System.IO;
using ScriptingHost;

namespace ScriptingHostDemoUI.ScriptConfigs
{
    class DefaultScriptConfig : IScriptConfig
    {
        public DefaultScriptConfig()
        {
            Name = "default";
            Paths = new string[]{};
            ScriptPacks = new IXScriptPack[] {};
                ReferenceAndImports= new Type[]{};
            ScriptParameters = new string[] {};
            NameSpaces = new string[] {};
        }

        public string Name { get; set; }
        public string[] Paths { get; set; }
        public IXScriptPack[] ScriptPacks { get; set; }
        public string[] ScriptParameters { get; set; }
        public Type[] ReferenceAndImports { get; set; }
        public string[] NameSpaces { get; set; }
    }
    
}
