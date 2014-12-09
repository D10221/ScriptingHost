namespace ScriptingHost
{
    public interface IScriptConfigManager
    {
        IScriptConfig GetConfig(IScriptInfo scriptName);
    }
}