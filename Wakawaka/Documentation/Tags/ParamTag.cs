using System;
using System.Xml.Linq;

namespace Wakawaka.Documentation.Tags
{
    /// <summary>
    /// Represents a &lt;param&gt; XML documentation tag.
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
        public ParamTag(XElement element) : base(element)
        {
            if (element.Attribute("name") == null)
                throw new ArgumentException(SR.MissingName);
        }

        /// <summary>
        /// Gets the name of the parameter the &lt;param&gt; tag describes.
        /// </summary>
        public string Name
        {
            get
            {
                return Element.Attribute("name").Value;
            }
        }

        /// <summary>
        /// Gets a string representation of the description of the parameter.
        /// </summary>
        /// <remarks>
        /// This property returns the concatenated text contents of the 
        /// &lt;param&gt; tag; use <see cref="Tag.Render(MarkdownTextWriter)"/>
        /// to properly render the tag's contents.
        /// </remarks>
        public string Description
        {
            get
            {
                return Element.Value;
            }
        }

        /// <summary>
        /// Returns a string representation of the &lt;param&gt; tag.
        /// </summary>
        /// <returns>
        /// A string containing the name and description of the &lt;param&gt; 
        /// tag.
        /// </returns>
        public override string ToString()
        {
            return Name + ": " + base.ToString();
        }
    }
}
