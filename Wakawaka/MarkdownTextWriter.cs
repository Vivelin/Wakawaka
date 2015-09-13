using System;
using System.IO;
using System.Text;

namespace Wakawaka
{
    /// <summary>
    /// Provides a text writer that can generate Markdown-formatted streams or
    /// files.
    /// </summary>
    public class MarkdownTextWriter : TextWriter
    {
        private const string CodeBlockFence = "```";
        private MarkdownTextWriterSettings settings;
        private TextWriter writer;

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownTextWriter"/>
        /// class using the specified <see cref="TextWriter"/>.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="TextWriter"/> object to write to.
        /// </param>
        public MarkdownTextWriter(TextWriter writer)
            : this(writer, MarkdownTextWriterSettings.Defaults)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownTextWriter"/>
        /// class using the specified <see cref="TextWriter"/> and settings.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="TextWriter"/> object to write to.
        /// </param>
        /// <param name="settings">
        /// A <see cref="MarkdownTextWriterSettings"/> object that controls the
        /// formatting of the new <see cref="MarkdownTextWriter"/>.
        /// </param>
        public MarkdownTextWriter(TextWriter writer,
            MarkdownTextWriterSettings settings)
        {
            this.writer = writer;
            this.settings = settings;
        }

        /// <summary>
        /// Gets the character encoding of the document being written.
        /// </summary>
        public override Encoding Encoding
        {
            get
            {
                return writer.Encoding;
            }
        }

        /// <summary>
        /// Gets the text writer that writes the formatted text.
        /// </summary>
        public TextWriter InnerWriter
        {
            get
            {
                return writer;
            }
        }

        /// <summary>
        /// Gets or sets the line terminator string used by the <see
        /// cref="MarkdownTextWriter"/>.
        /// </summary>
        public override string NewLine
        {
            get
            {
                return writer.NewLine;
            }
            set
            {
                writer.NewLine = value;
            }
        }

        /// <summary>
        /// Gets the <see cref="MarkdownTextWriterSettings"/> object that
        /// controls the formatting of this <see cref="MarkdownTextWriter"/>.
        /// </summary>
        public MarkdownTextWriterSettings Settings
        {
            get
            {
                return settings ?? MarkdownTextWriterSettings.Defaults;
            }
        }

        /// <summary>
        /// Encodes the specified string as HTML.
        /// </summary>
        /// <param name="value">The string to encode.</param>
        /// <returns>A string with special HTML entities escaped.</returns>
        /// <remarks>
        /// This function replaces only the characters ", &amp; &lt; and &gt;.
        /// </remarks>
        public static string Encode(string value)
        {
            var stringBuilder = new StringBuilder(value.Length);
            var i = 0;
            var c = '\0';
            while (i < value.Length)
            {
                c = value[i++];

                switch (c)
                {
                    case '"':
                        stringBuilder.Append("&quot;");
                        break;

                    case '&':
                        stringBuilder.Append("&amp;");
                        break;

                    case '<':
                        stringBuilder.Append("&lt;");
                        break;

                    case '>':
                        stringBuilder.Append("&gt;");
                        break;

                    default:
                        stringBuilder.Append(c);
                        break;
                }
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Closes the document being written.
        /// </summary>
        public override void Close()
        {
            writer.Close();
        }

        /// <summary>
        /// Flushes the document being written.
        /// </summary>
        public override void Flush()
        {
            writer.Flush();
        }

        /// <summary>
        /// Writes a character to the text string or stream.
        /// </summary>
        /// <param name="value">
        /// The character to write to the text stream.
        /// </param>
        /// <remarks>
        /// This is technically the only function we need to override, because
        /// all other <c>Write</c> and <c>WriteLine</c> methods eventually end
        /// up calling <see cref="TextWriter.Write(char)"/>.
        /// </remarks>
        public override void Write(char value)
        {
            writer.Write(value);
        }

        /// <summary>
        /// Writes a string to the text stream with special characters replaced
        /// by their corresponding HTML entities.
        /// </summary>
        /// <param name="value">The string to write to the text stream.</param>
        /// <seealso cref="Encode(string)"/>
        public override void Write(string value)
        {
            value = Encode(value);
            writer.Write(value);
        }

        /// <summary>
        /// Writes an ATX-style header to the text stream.
        /// </summary>
        /// <param name="value">
        /// The text to format as heading and write to the text stream.
        /// </param>
        /// <param name="level">
        /// The level of the heading specified as a value between 1 and 6.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="level"/> is less than 1 or greater than 6.
        /// </exception>
        public void WriteAtxHeader(string value, int level = 1)
        {
            if (level < 1 || level > 6)
            {
                throw new ArgumentOutOfRangeException(nameof(level),
                    SR.AtxHeaderOutOfRange);
            }

            for (var i = level; i > 0; i--)
                Write('#');
            Write(' ');
            WriteLine(value);
        }

        /// <summary>
        /// Writes an inline code span to the text stream.
        /// </summary>
        /// <param name="value">
        /// The text to format and write to the text stream.
        /// </param>
        public void WriteCode(string value)
        {
            if (value.Contains("`") && !value.Contains("``"))
            {
                Write("``");
                WriteRaw(value);
                Write("``");
            }
            else
            {
                Write('`');
                WriteRaw(value);
                Write('`');
            }
        }

        /// <summary>
        /// Writes a fenced code block to the text stream.
        /// </summary>
        /// <param name="value">
        /// The text to format and write to the text stream.
        /// </param>
        public void WriteCodeBlock(string value)
        {
            WriteLine(CodeBlockFence);
            WriteLineRaw(value);
            WriteLine(CodeBlockFence);
        }

        /// <summary>
        /// Writes a text with emphasis to the text stream.
        /// </summary>
        /// <param name="value">
        /// The text to format and write to the text stream.
        /// </param>
        public void WriteEmphasis(string value)
        {
            value = value.Replace("*", "\\*").Trim();
            Write('*');
            Write(value);
            Write('*');
        }

        /// <summary>
        /// Writes a Markdown-formatted heading to the text stream.
        /// </summary>
        /// <param name="value">
        /// The text to format and write to the text stream.
        /// </param>
        /// <param name="level">
        /// The level of the heading specified as a value between 1 and 6.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="level"/> is less than 1 or greater than 6.
        /// </exception>
        /// <remarks>
        /// For level 1 and 2 headings, the heading is formatted as a Setext
        /// header. For level 3 to 6 headers, the heading is formatted as an ATX
        /// header.
        /// </remarks>
        public void WriteHeading(string value, int level = 1)
        {
            if (level >= 1 && level <= 2)
            {
                WriteSetextHeader(value, level);
            }
            else if (level >= 3 && level <= 6)
            {
                WriteAtxHeader(value, level);
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(level),
                    SR.HeaderOutOfRange);
            }
        }

        /// <summary>
        /// Writes an explicit line break to the text stream.
        /// </summary>
        public void WriteLineBreak()
        {
            writer.WriteLine("  ");
        }

        /// <summary>
        /// Writes a string followed by a line terminator to the text stream.
        /// </summary>
        /// <param name="value">The string to write to the text stream.</param>
        public void WriteLineRaw(string value)
        {
            writer.WriteLine(value);
        }

        /// <summary>
        /// Writes a link to the text stream.
        /// </summary>
        /// <param name="destination">
        /// The link to write to the text stream.
        /// </param>
        public void WriteLink(string destination)
        {
            Write('<');
            Write(destination);
            Write('>');
        }

        /// <summary>
        /// Write a link with a description and an optional title to the text
        /// stream.
        /// </summary>
        /// <param name="value">The text to display for the link.</param>
        /// <param name="destination">The destination of the link.</param>
        /// <param name="title">The title of the link, or <c>null</c>.</param>
        public void WriteLink(string value, string destination, string title = null)
        {
            var enclose = destination.Contains(" ");

            Write('[');
            Write(value.Replace("]", "\\]"));
            Write(']');

            Write('(');
            if (enclose) Write('<');
            Write(destination.Delete('\n', '\r'));
            if (enclose) Write('>');

            if (title != null)
            {
                Write(' ');
                Write('"');
                Write(title.Replace("\"", "\\\"").Delete('\n', '\r'));
                Write('"');
            }
            Write(')');
        }

        /// <summary>
        /// Writes a string to the text stream.
        /// </summary>
        /// <param name="value">The string to write to the text stream.</param>
        public void WriteRaw(string value)
        {
            writer.Write(value);
        }

        /// <summary>
        /// Writes a Setext-style header to the text stream.
        /// </summary>
        /// <param name="value">
        /// The text to format as heading and write to the text stream.
        /// </param>
        /// <param name="level">
        /// The level of the heading specified as a value between 1 and 2.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="level"/> is less than 1 or greater than 2.
        /// </exception>
        public void WriteSetextHeader(string value, int level = 1)
        {
            if (level < 1 || level > 2)
            {
                throw new ArgumentOutOfRangeException(nameof(level),
                    SR.SetextHeaderOutOfRange);
            }

            char lineChar = '=';
            if (level == 2)
                lineChar = '-';

            WriteLine(value);
            for (var i = value.Length; i > 0; i--)
                Write(lineChar);
            WriteLine();
        }

        /// <summary>
        /// Writes a text with strong emphasis to the text stream.
        /// </summary>
        /// <param name="value">
        /// The text to format and write to the text stream.
        /// </param>
        public void WriteStrong(string value)
        {
            Write("**");
            Write(value);
            Write("**");
        }
    }
}
