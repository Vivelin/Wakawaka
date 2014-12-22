using System;
using System.Xml.Linq;
using Wakawaka.Documentation.Tags;

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
        /// <param name="member">
        /// The <see cref="XElement"/> object that contains the XML 
        /// documentation for the member.
        /// </param>
        protected Member(string id, XElement member)
        {
            ID = new ID(id);
            if (member.Element("summary") != null)
                Summary = Tag.Create(member.Element("summary"));
            if (member.Element("example") != null)
                Example = Tag.Create(member.Element("example"));
            if (member.Element("remarks") != null)
                Remarks = Tag.Create(member.Element("remarks"));
        }

        /// <summary>
        /// Gets the ID string that identifies the <see cref="Member"/>.
        /// </summary>
        public ID ID { get; protected set; }

        /// <summary>
        /// Gets a string that is used to describe a member.
        /// </summary>
        public Tag Summary { get; }

        /// <summary>
        /// Gets a string that is used to specify an example of how to use a 
        /// method or other library member.
        /// </summary>
        public Tag Example { get; }

        /// <summary>
        /// Gets a string that is used to add information about a type, 
        /// supplementing the information specified with <see cref="Summary"/>.
        /// </summary>
        public Tag Remarks { get; }

        /// <summary>
        /// Creates a new <see cref="Member"/> object for the specified 
        /// element.
        /// </summary>
        /// <param name="element">
        /// The <see cref="XElement"/> object that contains the documentation 
        /// for which to create a new <see cref="Member"/>.
        /// </param>
        /// <returns>
        /// A new <see cref="Member"/> object of the type corresponding to the 
        /// member described in <paramref name="element"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="element"/> has no <c>name</c> parameter, or the
        /// value of the <c>name</c> parameter is too short.
        /// </exception>
        /// <exception cref="Exception">
        /// <paramref name="element"/> belongs to a <c>namespace</c>, or the
        /// member being referenced is an error.
        /// </exception>
        /// <exception cref="NotImplementedException">
        /// <paramref name="element"/> references a kind of member that is not
        /// recognized.
        /// </exception>
        public static Member Create(XElement element)
        {
            var name = element.Attribute("name").Value;
            if (name == null) throw new ArgumentException(SR.MissingName);
            if (name.Length < 3) throw new ArgumentException(SR.NameTooShort);

            var prefix = name[0];
            switch (prefix)
            {
                case 'N': throw new Exception(SR.NoNamespaceDoc);
                case 'T': return new Type(name, element);
                case 'F': return new Field(name, element);
                case 'P': return new Property(name, element);
                case 'M': return new Method(name, element);
                case 'E': return new Event(name, element);
                case '!': throw new Exception(SR.NoErrorDoc);
                default:
                    throw new NotImplementedException(
                        SR.InvalidPrefix.FormatWith(prefix));
            }
        }

        /// <summary>
        /// Renders a Markdown representation of the <see cref="Member"/>.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="MarkdownTextWriter"/> object to write to.
        /// </param>
        public virtual void Render(MarkdownTextWriter writer)
        {
            RenderHeader(writer);

            if (Remarks != null)
            {
                writer.WriteHeading("Remarks", 2);
                Remarks.Render(writer);
                writer.WriteLine();
            }

            if (Example != null)
            {
                writer.WriteHeading("Examples", 2);
                Example.Render(writer);
                writer.WriteLine();
            }
        }

        /// <summary>
        /// Renders a Markdown-formatted representation of the <see 
        /// cref="Member"/>'s name and summary.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="MarkdownTextWriter"/> object to write to.
        /// </param>
        protected virtual void RenderHeader(MarkdownTextWriter writer)
        {
            writer.WriteHeading(ToString());
            Summary.Render(writer);
            writer.WriteLine();

            writer.WriteStrong("Namespace:");
            writer.Write(' ');
            writer.WriteLine(ID.Namespace);
            writer.WriteLine();
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
