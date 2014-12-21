using System;
using System.Xml.Linq;

namespace Wakawaka.Documentation
{
    /// <summary>
    /// Represents the XML documentation for a single member.
    /// </summary>
    public class Member
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Member"/> class with
        /// the specified ID string and XML documentation.
        /// </summary>
        /// <param name="id">The ID string that idenfities the member.</param>
        /// <param name="member">The <see cref="XElement"/> object that 
        /// contains the XML documenation for the member.</param>
        protected Member(string id, XElement member)
        {
            ID = new ID(id);
            if (member.Element("summary") != null)
                Summary = member.Element("summary").ToMarkdown();
            if (member.Element("example") != null)
                Example = member.Element("example").ToMarkdown();
            if (member.Element("remarks") != null)
                Remarks = member.Element("remarks").ToMarkdown();
        }

        /// <summary>
        /// Gets the ID string that identifies the <see cref="Member"/>.
        /// </summary>
        public ID ID { get; protected set; }

        /// <summary>
        /// Gets a string that is used to describe a member.
        /// </summary>
        public string Summary { get; protected set; }

        /// <summary>
        /// Gets a string that is used to specify an example of how to use a 
        /// method or other library member.
        /// </summary>
        public string Example { get; protected set; }

        /// <summary>
        /// Gets a string that is used to add information about a type, 
        /// supplementing the information specified with <see cref="Summary"/>.
        /// </summary>
        public string Remarks { get; protected set; }
        
        /// <summary>
        /// Creates a new <see cref="Member"/> object for the specified <see 
        /// cref="XElement"/>.
        /// </summary>
        /// <param name="element">The <see cref="XElement"/> object that 
        /// contains the documentation for which to create a new <see 
        /// cref="Member"/>.</param>
        /// <returns>A new <see cref="Member"/> object of the type 
        /// corresponding to the member described in <paramref name="element"/>.
        /// </returns>
        public static Member Create(XElement element)
        {
            var name = element.Attribute("name").Value;
            if (name == null)
                throw new ArgumentException("The specified element does not have a name attribute", "element");
            if (name.Length < 3)
                throw new ArgumentException("The length of the name attribute is too short", "element");

            var prefix = name[0];
            switch (prefix)
            {
                case 'N': throw new Exception("You cannot add documentation to a namespace");
                case 'T': return new Type(name, element);
                case 'F': return new Field(name, element);
                case 'P': return new Property(name, element);
                case 'M': return new Method(name, element);
                case 'E': return new Event(name, element);
                case '!': throw new Exception("You cannot add documentation to an error");
                default: return new Member(name, element);
            }
        }

        /// <summary>
        /// Renders a Markdown representation of the <see cref="Member"/>.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="System.IO.TextWriter"/> object to write to.
        /// </param>
        public virtual void Render(System.IO.TextWriter writer)
        {
            writer.WriteLine(Markdown.Heading(ToString()));
            writer.WriteLine(Summary);
        }

        /// <summary>
        /// Returns a string representation of the <see cref="Member"/>.
        /// </summary>
        /// <returns>A string containing the ID string.</returns>
        public override string ToString()
        {
            return ID.FullName;
        }
    }
}
