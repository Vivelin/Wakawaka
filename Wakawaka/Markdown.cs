using System;
using System.Text;
using System.Xml.Linq;

namespace Wakawaka
{
    /// <summary>
    /// Provides Markdown-related functionality and adds conversion 
    /// functionality to <see cref="System.Xml.Linq"/> classes.
    /// </summary>
    public static class Markdown
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
                builder.Append(Link(cref.Name, cref.FullName));
            }
            else if (node.Name == "paramref")
            {
                var name = node.Attribute("name").Value;
                builder.Append(Emphasis(name));
            }
            else if (node.Name == "c")
            {
                builder.Append(CodeSpan(node.Value));
            }
            else if (node.Name == "code")
            {
                builder.AppendLine(CodeBlock(node.Value));
            }
            else if (node.Name == "para")
            {
                builder.AppendLine();
                builder.AppendLine();
                builder.AppendLine(node.Value.Trim());

                // Don't trim the start of the string for obvious reasons
                return builder.ToString().TrimEnd(); 
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

        /// <summary>
        /// Formats the specified text as a Setext header, falling back to ATX 
        /// headers for level 3 to 6 headers.
        /// </summary>
        /// <param name="text">The text to format as a header.</param>
        /// <param name="level">The one-based level of the header to format.
        /// </param>
        /// <returns>A string formatted as a Setext header if <paramref 
        /// name="level"/> is equal to 1 or 2, or formatted as an ATX header if
        /// <paramref name="level"/> is between 3 or 6.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref 
        /// name="level"/> must be a value between 1 and 6.</exception>
        public static string Heading(string text, int level = 1)
        {
            switch (level)
            {
                case 1: return text.Underline('=');
                case 2: return text.Underline('-');
                case 3: return "### " + text;
                case 4: return "#### " + text;
                case 5: return "##### " + text;
                case 6: return "###### " + text;
                default: throw new ArgumentOutOfRangeException("level",
                    "level must be a value between 1 and 6.");
            }
        }

        /// <summary>
        /// Formats the specified text as an inline code span.
        /// </summary>
        /// <param name="text">The text to format as code.</param>
        /// <returns>A string containing the text enclosed by one or two 
        /// backticks.</returns>
        public static string CodeSpan(string text)
        {
            if (text.Contains("`") && !text.Contains("``"))
                return string.Format("``{0}``", text);
            return string.Format("`{0}`", text);
        }

        /// <summary>
        /// Formats the specified text as a fenced code block.
        /// </summary>
        /// <param name="text">The text to format as code.</param>
        /// <returns>A string containing the text enclosed by a code fence.
        /// </returns>
        public static string CodeBlock(string text)
        {
            var result = new StringBuilder();
            result.AppendLine("```");
            result.AppendLine(text);
            result.AppendLine("```");
            return result.ToString();
        }

        /// <summary>
        /// Formats the specified text with emphasis.
        /// </summary>
        /// <param name="text">The text to emphasise.</param>
        /// <returns>A string containing the text enclosed with emphasis marks.
        /// </returns>
        public static string Emphasis(string text)
        {
            // Escape the * with a backslash as per CommonMark spec:
            // http://spec.commonmark.org/0.7/#emphasis-and-strong-emphasis
            text = text.Replace("*", "\\*").Trim();

            return string.Format("*{0}*", text);
        }

        /// <summary>
        /// Formats the specified link as an autolink.
        /// </summary>
        /// <param name="target">The absolute URI to link to.</param>
        /// <returns>A new string containing a Markdown-formatted autolink.
        /// </returns>
        public static string Link(string target)
        {
            return string.Format("<{0}>", target);
        }

        /// <summary>
        /// Formats the specified text as an inline link.
        /// </summary>
        /// <param name="text">The text to display.</param>
        /// <param name="target">The link destination.</param>
        /// <param name="title">The title of the link, or <c>null</c>.</param>
        /// <returns>A new string containing a Markdown-formatted inline link.
        /// </returns>
        public static string Link(string text, string target, string title = null)
        {
            var label = text.Replace("]", "\\]");
            var destination = target;

            // If the destination contains spaces, it must be enclosed in 
            // pointy braces
            if (target.Contains(" "))
            {
                destination = string.Format("<{0}>", target);
            }

            // The title may be omitted
            if (title != null)
            {
                title = title.Replace("\"", "\\\"");
                destination += string.Format(" \"{0}\"", title);
            }

            // The destination cannot contain line breaks, even with pointy 
            // braces
            destination = destination.Delete('\n', '\r');

            return string.Format("[{0}]({1})", label, destination);
        }
    }
}
