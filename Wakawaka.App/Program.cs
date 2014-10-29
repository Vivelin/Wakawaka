using System;

namespace Wakawaka.App
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                var fileName = args[0];

                var xmlDoc = XmlDocumentation.Load(fileName);
                foreach (var member in xmlDoc.GetMembers())
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(member);
                    Console.ResetColor();
                    Console.WriteLine(member.Summary);
                    Console.WriteLine();
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
