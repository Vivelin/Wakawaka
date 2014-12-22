﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wakawaka.Documentation;

namespace Wakawaka.App
{
    /// <summary>
    /// Represents a project.
    /// </summary>
    class Project
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Project"/> class
        /// for the specified file name.
        /// </summary>
        /// <param name="fileName">
        /// The path to the .XML documentation file generated by Visual Studio.
        /// </param>
        public Project(string fileName)
        {
            Documentation = XmlDocumentation.Load(fileName);
        }

        /// <summary>
        /// Gets the XML documentation of the project.
        /// </summary>
        public XmlDocumentation Documentation { get; }

        /// <summary>
        /// Generates a formatted page for every type and member in the 
        /// <see cref="Project"/>.
        /// </summary>
        /// <param name="outputFolder">
        /// The name of the folder wherein the generated files will be stored.
        /// If it does not exist, it will be created.
        /// </param>
        public void GenerateDocumentation(string outputFolder)
        {
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            var members = from member in Documentation.GetMembers()
                          orderby member.ID.FullName ascending
                          select member;
            foreach (var member in members)
            {
                var path = Path.Combine(outputFolder, member.ID.FullName + ".md");
                var stream = new FileStream(path, FileMode.Create);
                var writer = new StreamWriter(stream);

                try
                {
                    RenderPage(member, writer);
                }
                finally
                {
                    writer.Close();
                }
            }
        }

        private void RenderPage(Member member, TextWriter writer)
        {
            var markdownWriter = new MarkdownTextWriter(writer);
            member.Render(markdownWriter);
        }
    }
}
