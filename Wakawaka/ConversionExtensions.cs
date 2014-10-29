using System.Text;
using System.Xml.Linq;

namespace Wakawaka
{
    /// <summary>
    /// Provides text conversion functionality to <see cref="System.Xml.Linq"/> 
    /// classes.
    /// </summary>
    public static class ConversionExtensions
    {
        /// <summary>
        /// Returns a Markdown representation of the <see cref="XNode"/> node.
        /// </summary>
        /// <param name="node">The <see cref="XNode"/> node to convert.</param>
        /// <returns>A Markdown-formatted string containing the contents of the
        /// abstract node.</returns>
        public static string ToMarkdown(this XNode node)
        {
            if (node is XText)
                return ((XText)node).ToMarkdown();

            if (node is XElement)
                return ((XElement)node).ToMarkdown();

            return node.ToString();
        }

        /// <summary>
        /// Returns a Markdown representation of the <see cref="XElement"/>.
        /// </summary>
        /// <param name="node">The <see cref="XElement"/> to convert.</param>
        /// <returns>A string containing the contents of the element, formatted
        /// as Markdown.</returns>
        public static string ToMarkdown(this XElement node)
        {
            var builder = new StringBuilder();

            if (node.Name == "see")
            {
                var cref = new ID(node.Attribute("cref"));
                builder.AppendFormat("[{0}]({1})", cref.DisplayName, cref.FullName);
            }
            else if (node.Name == "paramref")
            {
                var name = node.Attribute("name").Value;
                builder.AppendFormat("*{0}*", name);
            }
            else if (node.Name == "c")
            {
                builder.AppendFormat("`{0}`", node.Value.Compact());
            }
            else if (node.Name == "code")
            {
                builder.AppendLine("```");
                builder.AppendLine(node.Value);
                builder.AppendLine("```");
            }
            else
            {
                foreach (var child in node.Nodes())
                {
                    builder.Append(child.ToMarkdown());
                }
            }

            return builder.ToString().Trim();
        }

        /// <summary>
        /// Returns a Markdown representation of the <see cref="XText"/> node.
        /// </summary>
        /// <param name="node">The <see cref="XText"/> node to convert.</param>
        /// <returns>A Markdown-formatted string containing the contents of the
        /// text node.</returns>
        public static string ToMarkdown(this XText node)
        {
            return node.Value.Delete('\n', '\r').Squeeze();
        }
    }
}
