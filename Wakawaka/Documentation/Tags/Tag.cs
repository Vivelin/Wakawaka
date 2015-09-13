using System.Linq;
using System.Xml.Linq;

namespace Wakawaka.Documentation.Tags
{
    /// <summary>
    /// Represents a generic XML documentation tag.
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Tag"/> class for the
        /// specified element.
        /// </summary>
        /// <param name="element">
        /// The <see cref="XElement"/> object to represent.
        /// </param>
        public Tag(XElement element)
        {
            Element = element;
        }

        /// <summary>
        /// Gets the XML element that the tag represents.
        /// </summary>
        public XElement Element { get; }

        /// <summary>
        /// Gets the element name of the tag.
        /// </summary>
        public string ElementName
        {
            get
            {
                return Element.Name.ToString();
            }
        }

        /// <summary>
        /// Creates a new <see cref="Tag"/> instance depending on the name of
        /// the node.
        /// </summary>
        /// <param name="element">
        /// The <see cref="XElement"/> to create a <see cref="Tag"/> object
        /// from.
        /// </param>
        /// <returns>
        /// A newly created object that inherits from the <see cref="Tag"/>
        /// class.
        /// </returns>
        public static Tag Create(XElement element)
        {
            if (element == null) return null;

            switch (element.Name.LocalName)
            {
                case "c": return new CTag(element);
                case "code": return new CodeTag(element);
                case "exception": return new ExceptionTag(element);
                case "para": return new ParaTag(element);
                case "param": return new ParamTag(element);
                case "paramref": return new ParamRefTag(element);
                case "see": return new SeeTag(element);
                default: return new Tag(element);
            }
        }

        /// <summary>
        /// Renders a Markdown-formatted representation of the <see
        /// cref="Tag"/>.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="MarkdownTextWriter"/> object to write to.
        /// </param>
        public virtual void Render(MarkdownTextWriter writer)
        {
            var children = Element.Nodes();

            // Render the first child to a separate writer, so that we can
            // eliminate the leading white-space before rendering it.
            var first = children.FirstOrDefault();
            if (first != null)
            {
                using (var stringWriter = new System.IO.StringWriter())
                {
                    var interceptor = new MarkdownTextWriter(stringWriter);
                    RenderChildNode(interceptor, first);

                    var interceptedString = stringWriter.ToString();
                    writer.WriteRaw(interceptedString.TrimStart());
                }
            }

            // Render the rest normally.
            foreach (var child in children.Skip(1))
            {
                RenderChildNode(writer, child);
            }

            writer.WriteLine();
        }

        /// <summary>
        /// Returns a string representation of the tag.
        /// </summary>
        /// <returns>
        /// A string that contains the XML contents of the tag.
        /// </returns>
        public override string ToString()
        {
            return Element.ToString();
        }

        private static void RenderChildNode(MarkdownTextWriter writer, XNode node)
        {
            if (node is XElement)
            {
                var tag = Create((XElement)node);
                tag.Render(writer);
            }
            else if (node is XText)
            {
                var text = ((XText)node).Value;
                writer.Write(text.ToSingleLine());
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Unsupported {0} node",
                    node.GetType());
                writer.Write(node.ToString());
            }
        }
    }
}
