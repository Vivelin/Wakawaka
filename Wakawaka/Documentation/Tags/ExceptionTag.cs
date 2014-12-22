using System.Xml.Linq;

namespace Wakawaka.Documentation.Tags
{
    /// <summary>
    /// Represents an <c>&lt;exception&gt;</c> XML documentation tag.
    /// </summary>
    public class ExceptionTag : Tag
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionTag"/> class
        /// for the specified element.
        /// </summary>
        /// <param name="element">
        /// The <see cref="XElement"/> object to represent.
        /// </param>
        public ExceptionTag(XElement element) : base(element)
        {
            CRef = new ID(Element.Attribute("cref"));
        }

        /// <summary>
        /// Gets an <see cref="ID"/> object that represents the reference to 
        /// the exception type.
        /// </summary>
        public ID CRef { get; }
    }
}
