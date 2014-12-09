namespace ScriptingHost
{
    public interface IScriptInfo
    {
        string FullPath { get; set; }
        string Name { get; set; }
        string Directory { get; set; }
        string Extension { get; set; }
        string ConfigName { get; set; }
    }
}