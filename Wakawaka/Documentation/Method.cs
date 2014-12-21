using System.Xml.Linq;

namespace Wakawaka.Documentation
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
        /// Renders a Markdown representation of the <see cref="Method"/>.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="System.IO.TextWriter"/> object to write to.
        /// </param>
        public override void Render(System.IO.TextWriter writer)
        {
            writer.WriteLine(Markdown.Heading(ToString()));
            writer.WriteLine(Summary);
            writer.WriteLine();
            if (ReturnValue != null)
            {
                writer.WriteLine(Markdown.Heading("Return Value", 3));
                writer.WriteLine(ReturnValue);
                writer.WriteLine();
            }

            // Parameters

            if (Remarks != null)
            {
                writer.WriteLine(Markdown.Heading("Remarks", 2));
                writer.WriteLine(Remarks);
                writer.WriteLine();
            }
            if (Example != null)
            {
                writer.WriteLine(Markdown.Heading("Examples", 2));
                writer.WriteLine(ReturnValue);
                writer.WriteLine();
            }

            // See Also
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
