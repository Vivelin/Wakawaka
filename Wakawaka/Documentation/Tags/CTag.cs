using System.Xml.Linq;

namespace Wakawaka.Documentation.Tags
{
    /// <summary>
    /// Represents a &lt;c&gt; XML documentation tag.
    /// </summary>
    public class CTag : Tag
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CTag"/> class for the
        /// specified element.
        /// </summary>
        /// <param name="element">
        /// The <see cref="XElement"/> object to represent.
        /// </param>
        public CTag(XElement element) : base(element)
        {
        }

        /// <summary>
        /// Gets the text that is indicated as code.
        /// </summary>
        public string Value
        {
            get
            {
                return Element.Value;
            }
        }

        /// <summary>
        /// Renders a Markdown-formatted representation of the &lt;c&gt; tag.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="MarkdownTextWriter"/> object to write to.
        /// </param>
        public override void Render(MarkdownTextWriter writer)
        {
            writer.WriteCode(Value);
        }

        /// <summary>
        /// Returns a string representation of the &lt;c&gt; tag.
        /// </summary>
        /// <returns>
        /// A string containing the text contents of the &lt;c&gt; tag.
        /// </returns>
        public override string ToString()
        {
            return Value;
        }
    }
}
