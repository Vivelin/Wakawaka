using System;
using System.IO;
using System.Linq;

namespace Wakawaka.App
{
    class Program
    {
        private static string outputFolder;

        static void Main(string[] args)
        {
            Console.Title = "Wakawaka";

            if (args.Length > 0)
            {
                var fileName = args[0];
                outputFolder = args?[1] ?? Environment.CurrentDirectory;

                if (!Directory.Exists(outputFolder))
                    Directory.CreateDirectory(outputFolder);

                var xmlDoc = XmlDocumentation.Load(fileName);
                var types = from member in xmlDoc.GetMembers()
                            where member is Documentation.Type
                            orderby member.ID.FullName ascending
                            select member;

                foreach (var member in types)
                {
                    RenderMember(member);
                }
            }
            else
            {
                Console.Error.WriteLine("Usage: {0} filename",
                    AppDomain.CurrentDomain.FriendlyName);
            }
        }

        private static void RenderMember(Documentation.Member member)
        {
            var path = GenerateFileName(member);
            var stream = new FileStream(path, FileMode.OpenOrCreate);
            var writer = new StreamWriter(stream);

            try
            {
                var markdownWriter = new MarkdownTextWriter(writer);
                member.Render(markdownWriter);
            }
            finally
            {
                writer.Close();
            }
        }

        private static string GenerateFileName(Documentation.Member member)
        {
            var name = member.ID.FullName + ".md";
            return Path.Combine(outputFolder, name);
        }
    }
}
