using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using Wakawaka.Documentation;

namespace Wakawaka
{
    /// <summary>
    /// Represents the XML documentation for a project.
    /// </summary>
    /// <example>
    /// The following example shows how to use the XmlDocumentation class to 
    /// load an XML file and print Markdown-formatted documentation for all
    /// types to the console.
    /// 
    /// <code><![CDATA[using System;
    /// using System.Linq;
    /// 
    /// using Wakawaka;
    /// 
    /// namespace Example
    /// {
    ///     class Program
    ///     {
    ///         static void Main(string[] args)
    ///         {
    ///             var xmlDocumentation = XmlDocumentation.Load(@"Example.XML");
    ///             var types = from member in xmlDocumentation.GetMembers()
    ///                         where member is Documentation.Type
    ///                         select member;
    ///             foreach (var member in types)
    ///             {
    ///                 using (var stringWriter = new System.IO.StringWriter())
    ///                 {
    ///                     var writer = new MarkdownTextWriter(stringWriter);
    ///                     member.Render(writer);
    ///                     Console.WriteLine(stringWriter.ToString());
    ///                 }
    ///             }
    ///         }
    ///     }
    /// }
    /// ]]></code>
    /// </example>
    public class XmlDocumentation
    {
        private XDocument document;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlDocumentation"/>
        /// class using the specified <see cref="XDocument"/>.
        /// </summary>
        /// <param name="document">The <see cref="XDocument"/> whose content to
        /// load.</param>
        public XmlDocumentation(XDocument document)
        {
            this.document = document;
        }

        /// <summary>
        /// Creates a new <see cref="XmlDocumentation"/> from the specified 
        /// file or URI.
        /// </summary>
        /// <param name="path">
        /// A string containing the URI or path to the file to load.
        /// </param>
        /// <returns>
        /// A new <see cref="XmlDocumentation"/> object that represents the 
        /// contents of the specified file.
        /// </returns>
        public static XmlDocumentation Load(string path)
        {
            var document = XDocument.Load(path);
            return new XmlDocumentation(document);
        }

        /// <summary>
        /// Creates a new <see cref="XmlDocumentation"/> from the specified
        /// string.
        /// </summary>
        /// <param name="content">
        /// A string containing the XML text to load.
        /// </param>
        /// <returns>
        /// A new <see cref="XmlDocumentation"/> object that represents the 
        /// specified XML text.
        /// </returns>
        public static XmlDocumentation Parse(string content)
        {
            var document = XDocument.Parse(content);
            return new XmlDocumentation(document);
        }

        /// <summary>
        /// Returns a collection of <see cref="Member"/> objects in the <see 
        /// cref="XmlDocumentation"/>.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> collection of <see cref="Member"/> 
        /// objects.
        /// </returns>
        public IEnumerable<Member> GetMembers()
        {
            var elements = from member in document.Descendants("member")
                           select member;
            foreach (var element in elements)
            {
                yield return Member.Create(element);
            }
        }
    }
}
