using System;
using System.IO;
using System.Linq;

namespace Wakawaka.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Wakawaka";

            if (args.Length > 1)
            {
                var fileName = args[0];
                var repo = args[1];
                var uri = "https://github.com/\{repo}.wiki.git";
                var workFolder = GetTempWorkDir(repo);

                var wiki = new Wiki(uri, workFolder);
                var project = new Project(fileName);
                wiki.Publish(project);
            }
            else
            {
                Console.Error.WriteLine("Usage: {0} filename",
                    AppDomain.CurrentDomain.FriendlyName);
            }
        }

        static string GetTempWorkDir(string repo = null)
        {
            var path = Path.Combine(Path.GetTempPath(), "Wakawaka");
            if (repo == null)
                repo = Path.GetRandomFileName();
            path = Path.Combine(path, repo);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return Path.GetFullPath(path);
        }
    }
}
