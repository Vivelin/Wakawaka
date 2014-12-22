using System.Xml.Linq;

namespace Wakawaka.Documentation.Tags
{
    /// <summary>
    /// Represents a &lt;code&gt; XML documentation tag.
    /// </summary>
    public class CodeTag : Tag
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CodeTag"/> class for 
        /// the specified element.
        /// </summary>
        /// <param name="element">
        /// The <see cref="XElement"/> object to represent.
        /// </param>
        public CodeTag(XElement element) : base(element)
        {
        }

        /// <summary>
        /// Gets the text that is marked as code.
        /// </summary>
        public string Value
        {
            get
            {
                return Element.Value;
            }
        }

        /// <summary>
        /// Renders a Markdown-formatted representation of the &lt;code&gt; tag.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="MarkdownTextWriter"/> object to write to.
        /// </param>
        public override void Render(MarkdownTextWriter writer)
        {
            writer.WriteLine();
            writer.WriteCodeBlock(Value);
        }

        /// <summary>
        /// Returns a string representation of the &lt;code&gt; tag.
        /// </summary>
        /// <returns>
        /// A string containing the text contents of the &lt;code&gt; tag.
        /// </returns>
        public override string ToString()
        {
            return Value;
        }
    }
}
