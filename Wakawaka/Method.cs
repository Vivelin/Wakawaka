using System.Text;
using System.Xml.Linq;

namespace Wakawaka
{
    /// <summary>
    /// Represents the XML documentation for a method.
    /// </summary>
    public class Method : Member
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Method"/> class with
        /// the specified ID string and XML documentation.
        /// </summary>
        /// <param name="id">The ID string that idenfities the method.</param>
        /// <param name="member">The <see cref="XElement"/> object that 
        /// contains the XML documenation for the method.</param>
        public Method(string id, XElement member)
            : base(id, member)
        {
            if (member.Element("returns") != null)
                ReturnValue = member.Element("returns").ToMarkdown();
        }

        /// <summary>
        /// Gets a string that is used to describe the return value of the
        /// method.
        /// </summary>
        public string ReturnValue { get; protected set; }

        /// <summary>
        /// Gets a value indicating whether the method is a constructor.
        /// </summary>
        public bool IsConstructor
        {
            get { return ID.Name == ".ctor"; }
        }

        /// <summary>
        /// Returns a Markdown representation of the <see cref="Method"/>.
        /// </summary>
        /// <returns>A string containing the Markdown-formatted contents of the
        /// <see cref="Method"/>.</returns>
        public override string ToMarkdown()
        {
            var content = new StringBuilder();
            content.AppendLine(Markdown.Heading(ToString()));
            content.AppendLine(Summary);
            content.AppendLine();
            if (ReturnValue != null)
            {
                content.AppendLine(Markdown.Heading("Return Value", 3));
                content.AppendLine(ReturnValue);
                content.AppendLine();
            }

            // Parameters

            if (Remarks != null)
            {
                content.AppendLine(Markdown.Heading("Remarks", 2));
                content.AppendLine(Remarks);
                content.AppendLine();
            }
            if (Example != null)
            {
                content.AppendLine(Markdown.Heading("Examples", 2));
                content.AppendLine(ReturnValue);
                content.AppendLine();
            }
            // See Also
            return content.ToString();
        }

        /// <summary>
        /// Returns a string representation of the <see cref="Method"/>.
        /// </summary>
        /// <returns>A string containing the name of the method.</returns>
        public override string ToString()
        {
            if (IsConstructor)
                return string.Format("{0} Constructor", ID.ClassName);
            return string.Format("{0}.{1} Method", ID.ClassName, ID.Name);
        }
    }
}
