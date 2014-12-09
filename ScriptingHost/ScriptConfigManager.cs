using System.Collections.Generic;
using System.Linq;

namespace ScriptingHost
{
    public class ScriptConfigManager : IScriptConfigManager
    {
        readonly IEnumerable<IScriptConfig> _configs;
        
        private readonly IScriptConfig _defaulConfig;

        public ScriptConfigManager(IEnumerable<IScriptConfig> configs)
        {
            _configs = configs.ToArray();

            _defaulConfig = _configs.FirstOrDefault(c => c.Name == "default");
            
        }

        public IScriptConfig GetConfig(IScriptInfo scriptInfo)
        {            
            return _configs.FirstOrDefault(c => c.Name == scriptInfo.ConfigName ) ?? _defaulConfig;
        }
    }
}