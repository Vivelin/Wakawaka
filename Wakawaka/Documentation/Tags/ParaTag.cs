using System;
using System.Xml.Linq;

namespace Wakawaka.Documentation.Tags
{
    /// <summary>
    /// Represents a <c>&lt;para&gt;</c> XML documentation tag.
    /// </summary>
    public class ParaTag : Tag
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParaTag"/> class for 
        /// the specified element.
        /// </summary>
        /// <param name="element">
        /// The <see cref="XElement"/> object to represent.
        /// </param>
        public ParaTag(XElement element) : base(element)
        {
        }

        /// <summary>
        /// Gets the text of the paragraph.
        /// </summary>
        public string Text
        {
            get
            {
                return Element.Value.ToSingleLine().Trim();
            }
        }

        /// <summary>
        /// Renders a Markdown-formatted representation of the 
        /// <c>&lt;code&gt;</c> tag.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="MarkdownTextWriter"/> object to write to.
        /// </param>
        public override void Render(MarkdownTextWriter writer)
        {
            writer.WriteLine();
            writer.WriteLine();
            writer.WriteLine(Text);
        }
    }
}
