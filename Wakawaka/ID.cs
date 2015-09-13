using System;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Wakawaka
{
    /// <summary>
    /// Represents an ID string or a code reference in an XML documentation tag.
    /// </summary>
    public class ID
    {
        /// <summary>
        /// A regular expression that parses ID strings.
        /// </summary>
        private readonly Regex Parser =
            new Regex(@"^(?<P>[NTFPME!]):(?<N>(?:[^*@^|![(\s]+))(?<E>.*)$");

        private string Identifier;

        /// <summary>
        /// Initializes a new instance of the <see cref="ID"/> class for the
        /// specified cref <see cref="XAttribute"/>.
        /// </summary>
        /// <param name="cref">The cref <see cref="XAttribute"/>.</param>
        public ID(XAttribute cref)
        {
            if (cref == null) throw new ArgumentNullException("cref");

            Identifier = cref.Value;
            Parse(Identifier);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ID"/> class for the
        /// specified ID string.
        /// </summary>
        /// <param name="identifier">A string containing the ID string.</param>
        public ID(string identifier)
        {
            if (identifier == null) throw new ArgumentNullException("identifier");

            this.Identifier = identifier;
            Parse(identifier);
        }

        /// <summary>
        /// Represents the kinds of members that can be identified.
        /// </summary>
        public enum MemberType
        {
            /// <summary>
            /// Indicates a lack of a prefix.
            /// </summary>
            None = '\0',

            /// <summary>
            /// Indicates a namespace. You cannot add documentation comments to
            /// a namespace, but you can make cref references to them, where
            /// supported.
            /// </summary>
            Namespace = 'N',

            /// <summary>
            /// Indicates a type, e.g. class, interface, struct, enum, or
            /// delegate.
            /// </summary>
            Type = 'T',

            /// <summary>
            /// Indicates a field.
            /// </summary>
            Field = 'F',

            /// <summary>
            /// Indicates a property, including indexers.
            /// </summary>
            Property = 'P',

            /// <summary>
            /// Indicates a method, including special methods such as
            /// constructors and operators.
            /// </summary>
            Method = 'M',

            /// <summary>
            /// Indicates an event.
            /// </summary>
            Event = 'E',

            /// <summary>
            /// Indicates an error that the identifier cannot be resolved.
            /// </summary>
            Error = '!'
        }

        /// <summary>
        /// Gets any additional unparsed information present in the ID string.
        /// </summary>
        public string Arguments { get; protected set; }

        /// <summary>
        /// If the member being identified is not a type or namespace, gets the
        /// name of the class it exists in.
        /// </summary>
        public string ClassName { get; protected set; }

        /// <summary>
        /// Gets the fully qualified name of the member being identified,
        /// starting at the root of the namespace. Periods in names have been
        /// replaced with `#`.
        /// </summary>
        public string FullName { get; protected set; }

        /// <summary>
        /// Gets the name of the member being identified.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets the namespace the member being identified exists in.
        /// </summary>
        public string Namespace { get; protected set; }

        /// <summary>
        /// Gets a value indicating the kind of member being identified.
        /// </summary>
        public MemberType Prefix { get; protected set; }

        /// <summary>
        /// Converts the specified string to an <see cref="ID"/> string.
        /// </summary>
        /// <param name="identifier">The string to convert.</param>
        /// <returns>The converted <see cref="ID"/> string.</returns>
        public static implicit operator ID(string identifier)
        {
            return new ID(identifier);
        }

        /// <summary>
        /// Converts the specified <see cref="ID"/> string to a <see
        /// cref="String"/>.
        /// </summary>
        /// <param name="id">The ID string to convert.</param>
        /// <returns>The converted <see cref="String"/>.</returns>
        public static implicit operator string (ID id)
        {
            return id.ToString();
        }

        /// <summary>
        /// Renders a Markdown-formatted representation of the code reference.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="MarkdownTextWriter"/> object to write to.
        /// </param>
        public void Render(MarkdownTextWriter writer)
        {
            writer.WriteLink(FullName, FullName);
        }

        /// <summary>
        /// Returns a string representation of the <see cref="ID"/> string.
        /// </summary>
        /// <returns>
        /// A string containing the fully qualified ID string.
        /// </returns>
        public override string ToString()
        {
            return Identifier;
        }

        /// <summary>
        /// Parses the specified ID string.
        /// </summary>
        /// <param name="identifier">The ID string to parse.</param>
        protected void Parse(string identifier)
        {
            var match = Parser.Match(identifier);
            if (match.Success)
            {
                var prefix = match.Groups["P"].Value[0];
                var name = match.Groups["N"].Value;
                var extra = match.Groups["E"].Value;

                Prefix = (MemberType)prefix;
                FullName = name;
                Arguments = extra;

                var parts = name.Split('.').Replace("#", ".");
                var index = parts.Length - 1;

                Name = parts[index--];
                if (Prefix != MemberType.Namespace && Prefix != MemberType.Type)
                    ClassName = parts[index--];
                Namespace = string.Join(".", parts, 0, index + 1);
            }
        }
    }
}
