using System.Xml.Linq;

namespace Wakawaka.Documentation.Tags
{
    /// <summary>
    /// Represents a <c>&lt;paramref&gt;</c> XML documentation tag.
    /// </summary>
    public class ParamRefTag : Tag
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParamRefTag"/> class
        /// for the specified element.
        /// </summary>
        /// <param name="element">
        /// The <see cref="XElement"/> object to represent.
        /// </param>
        public ParamRefTag(XElement element) : base(element)
        {
            Name = element.Attribute("name").Value.Trim();
        }

        /// <summary>
        /// Gets the name of the parameter that is referred to.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Renders a Markdown-formatted representation of the &lt;paramref&gt;
        /// tag.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="MarkdownTextWriter"/> object to write to.
        /// </param>
        public override void Render(MarkdownTextWriter writer)
        {
            writer.WriteEmphasis(Name);
        }
    }
}
