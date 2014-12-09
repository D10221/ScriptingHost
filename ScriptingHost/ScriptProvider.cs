using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace ScriptingHost
{
    public class ScriptProvider : IScriptProvider
    {       
        public Task<string> GetScriptContent(IScriptInfo scriptInfo)
        {
            var path = scriptInfo.FullPath ?? GetPath(scriptInfo.Name+scriptInfo.Extension);

            return ReadAllTextAsync(path);
        }

        private async Task<string> ReadAllTextAsync(string path)
        {
            string text;

            using (var stream = new FileStream(path, FileMode.Open))
            {
                using (var reader = new StreamReader(stream))
                {
                   text =  await reader.ReadToEndAsync();
                }
            }

            return text;
        }

        private static string GetPath(string scriptName)
        {
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\scripts\\" + scriptName + ".csx";
            return path;
        }

        public async Task<bool> SaveAsync(string scriptName,string content)
        {
            var path = GetPath(scriptName);

            try
            {
                using (var stream = new FileStream(path, FileMode.OpenOrCreate))
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        await writer.WriteAsync(content);
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return File.Exists(path);
        }
    }
}