namespace ScriptingHost
{
    public class ScriptInfo : IScriptInfo
    {
        public string FullPath { get; set; }
        public string Name { get; set; }
        public string Directory { get; set; }
        public string Extension { get; set; }
        public string ConfigName { get; set; }
    }
}