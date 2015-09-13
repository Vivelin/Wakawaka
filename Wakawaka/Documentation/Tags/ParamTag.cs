using System;
using System.Xml.Linq;

namespace Wakawaka.Documentation.Tags
{
    /// <summary>
    /// Represents a <c>&lt;param&gt;</c> XML documentation tag.
    /// </summary>
    public class ParamTag : Tag
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParamTag"/> class for
        /// the specified element.
        /// </summary>
        /// <param name="element">
        /// The <see cref="XElement"/> object to represent.
        /// </param>
        /// <exception cref="ArgumentException">
        /// The <paramref name="element"/> does not have a <c>name</c>
        /// attribute.
        /// </exception>
        public ParamTag(XElement element) : base(element)
        {
            if (element.Attribute("name") == null)
                throw new ArgumentException(SR.MissingName);
        }

        /// <summary>
        /// Gets a string representation of the description of the parameter.
        /// </summary>
        /// <remarks>
        /// This property returns the concatenated text contents of the
        /// <c>&lt;param&gt;</c> tag; use <see
        /// cref="Tag.Render(MarkdownTextWriter)"/> to properly render the tag's
        /// contents.
        /// </remarks>
        public string Description
        {
            get
            {
                return Element.Value;
            }
        }

        /// <summary>
        /// Gets the name of the parameter the <c>&lt;param&gt;</c> tag
        /// describes.
        /// </summary>
        public string Name
        {
            get
            {
                return Element.Attribute("name").Value;
            }
        }
    }
}
