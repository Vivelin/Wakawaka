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
    /// WHAT THE GOD DAMN FUCK IS YOUR MAJOR MALFUNCTION! THIS GAME IS FUCKING 
    /// RATED M FOR MATURE! THIS ISN'T SOME FUCKING CHRISTIAN SERVER, YOU WANT 
    /// A NO CUSSING CLUB GO TO A SERVER THAT DOESN'T ALLOW IT! THERE ARE NOT 
    /// ENOUGH MOTHERFUCKERS TO PATROL THE SERVERS! I'M NOT GOING TO SIT ON 
    /// SERVER AND BAN EVERYONE THAT SAYS GODDAMN, DAMN, SHIT, FUCK, CUNT, 
    /// BITCH, MOTHERFUCKER, ASS, AND ANY OTHER VARIETIES OUT THERE.
    /// <para>
    /// SO YOU KNOW WHAT FUCKING DEAL WITH THIS SHIT, BITCH.
    /// </para>
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
        /// <param name="path">A string containing the URI or path to the file
        /// to load.</param>
        /// <returns>A new <see cref="XmlDocumentation"/> object that 
        /// represents the contents of the specified file.</returns>
        public static XmlDocumentation Load(string path)
        {
            var document = XDocument.Load(path);
            return new XmlDocumentation(document);
        }

        /// <summary>
        /// Creates a new <see cref="XmlDocumentation"/> from the specified
        /// string.
        /// </summary>
        /// <param name="content">A string containing the XML text to load.
        /// </param>
        /// <returns>A new <see cref="XmlDocumentation"/> object that 
        /// represents the specified XML text.</returns>
        public static XmlDocumentation Parse(string content)
        {
            var document = XDocument.Parse(content);
            return new XmlDocumentation(document);
        }

        /// <summary>
        /// Returns a collection of <see cref="Member"/> objects in the <see 
        /// cref="XmlDocumentation"/>.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> collection of <see 
        /// cref="Member"/> objects.</returns>
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
