using System.Text;
using System.Xml.Linq;

namespace Wakawaka
{
    /// <summary>
    /// Represents the XML documentation for a type (e.g. class, interface, 
    /// structure, enum or delegate).
    /// </summary>
    public class Type : Member
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Type"/> class with the
        /// specified ID string and XML documentation.
        /// </summary>
        /// <param name="id">The ID string that idenfities the member.</param>
        /// <param name="member">The <see cref="XElement"/> object that 
        /// contains the XML documenation for the member.</param>
        public Type(string id, XElement member)
            : base(id, member) { }

        /// <summary>
        /// Returns a Markdown representation of the <see cref="Type"/>.
        /// </summary>
        /// <returns>A string containing the Markdown-formatted contents of the
        /// <see cref="Type"/>.</returns>
        public override string ToMarkdown()
        {
            var content = new StringBuilder();
            content.AppendLine(Markdown.Heading(ToString()));
            content.AppendLine(Summary);
            content.AppendLine();

            if (Example != null)
            {
                content.AppendLine(Markdown.Heading("Examples", 2));
                content.AppendLine(Example);
            }
            return content.ToString();
        }

        /// <summary>
        /// Returns a string representation of the <see cref="Type"/>.
        /// </summary>
        /// <returns>A string containing the name of the type.</returns>
        public override string ToString()
        {
            return string.Format("{0} Type", ID.Name);
        }
    }
}
