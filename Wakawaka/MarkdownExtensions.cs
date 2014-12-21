using System.Xml.Linq;

namespace Wakawaka
{
    // TODO: Find a better way to do this
    // Probably ends up being something like a class for every type of tag
    // which each has a Render(MarkdownTextWriter) function
    /// <summary>
    /// Provides methods that render common XML documentation tags to Markdown.
    /// </summary>
    public static class MarkdownExtensions
    {
        /// <summary>
        /// Returns a Markdown representation of the <see cref="XNode"/> node.
        /// </summary>
        /// <param name="node">
        /// The <see cref="XNode"/> node to convert.
        /// </param>
        /// <returns>
        /// A Markdown-formatted string containing the contents of the abstract 
        /// node.
        /// </returns>
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
        /// <param name="node">
        /// The <see cref="XElement"/> to convert.
        /// </param>
        /// <returns>
        /// A string containing the contents of the element, formatted as 
        /// Markdown.
        /// </returns>
        public static string ToMarkdown(this XElement node)
        {
            using (var stringWriter = new System.IO.StringWriter())
            {
                var writer = new MarkdownTextWriter(stringWriter);

                if (node.Name == "see")
                {
                    var cref = new ID(node.Attribute("cref"));
                    writer.WriteLink(cref.Name, cref.FullName);
                }
                else if (node.Name == "paramref")
                {
                    var name = node.Attribute("name").Value;
                    writer.WriteEmphasis(name);
                }
                else if (node.Name == "c")
                {
                    writer.WriteCode(node.Value);
                }
                else if (node.Name == "code")
                {
                    writer.WriteCodeBlock(node.Value);
                }
                else if (node.Name == "para")
                {
                    writer.WriteLine();
                    writer.WriteLine();
                    writer.WriteLine(node.Value.Trim());

                    // Don't trim the start of the string for obvious reasons
                    return stringWriter.ToString().TrimEnd();
                }
                else
                {
                    foreach (var child in node.Nodes())
                    {
                        writer.Write(child.ToMarkdown());
                    }
                }

                return stringWriter.ToString().Trim();
            }
        }

        /// <summary>
        /// Returns a Markdown representation of the <see cref="XText"/> node.
        /// </summary>
        /// <param name="node">
        /// The <see cref="XText"/> node to convert.
        /// </param>
        /// <returns>
        /// A Markdown-formatted string containing the contents of the text 
        /// node.
        /// </returns>
        public static string ToMarkdown(this XText node)
        {
            return node.Value.Delete('\n', '\r').Squeeze();
        }
    }
}
