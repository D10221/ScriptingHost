using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ScriptingHost;

namespace ScriptingHostDemoUI
{
    public class ScriptManager
    {
        public ScriptManager()
        {
            Scripts = Directory.EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory + "\\scripts", "*.csx").Select(path=> new ScriptInfo
            {
                FullPath = path, 
                Name = Path.GetFileNameWithoutExtension(path), 
                Directory = Path.GetDirectoryName(path), 
                Extension = Path.GetExtension(path)
            });
        }

        public readonly IEnumerable<ScriptInfo> Scripts;
    }
}
