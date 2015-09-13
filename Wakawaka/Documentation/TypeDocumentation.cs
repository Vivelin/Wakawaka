using System;
using System.Xml.Linq;

namespace Wakawaka.Documentation
{
    /// <summary>
    /// Represents the XML documentation for a type (e.g. class, interface,
    /// structure, enum or delegate).
    /// </summary>
    public class TypeDocumentation : MemberDocumentation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeDocumentation"/>
        /// class with the specified ID string and XML documentation.
        /// </summary>
        /// <param name="id">The ID string that idenfities the member.</param>
        /// <param name="member">
        /// The <see cref="XElement"/> object that contains the XML
        /// documentation for the member.
        /// </param>
        public TypeDocumentation(string id, XElement member)
            : base(id, member)
        { }

        /// <summary>
        /// Renders a Markdown representation of the <see
        /// cref="TypeDocumentation"/>.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="MarkdownTextWriter"/> object to write to.
        /// </param>
        public override void Render(MarkdownTextWriter writer)
        {
            base.Render(writer);

            if (Example != null)
            {
                writer.WriteHeading("Examples", 2);
                Example.Render(writer);
                writer.WriteLine();
            }
        }

        /// <summary>
        /// Returns a string representation of the <see
        /// cref="TypeDocumentation"/>.
        /// </summary>
        /// <returns>A string containing the name of the type.</returns>
        public override string ToString()
        {
            return String.Format("{0} Type", ID.Name);
        }
    }
}
