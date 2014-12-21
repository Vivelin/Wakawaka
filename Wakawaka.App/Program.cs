using System;
using System.IO;
using System.Linq;

namespace Wakawaka.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title ="Wakawaka";

            if (args.Length > 0)
            {
                var fileName = args[0];

                var xmlDoc = XmlDocumentation.Load(fileName);

                var types = from member in xmlDoc.GetMembers()
                              where member.ID.Prefix == ID.MemberType.Type
                              orderby member.ID.FullName ascending
                              select member;

                foreach (var member in types)
                {
                    using (var writer = new StringWriter())
                    {
                        member.Render(writer);

                        Console.WriteLine(writer.ToString());
                        Console.WriteLine();
                    }
                }
            }
            else
            {
                Console.Error.WriteLine("Usage: {0} filename", 
                    AppDomain.CurrentDomain.FriendlyName);
            }
        }
    }
}
