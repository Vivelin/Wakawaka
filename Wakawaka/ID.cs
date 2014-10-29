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
        /// Gets a regular expression that parses ID strings.
        /// </summary>
        protected readonly Regex parser =
            new Regex(@"^(?<P>[NTFPME!]):(?<N>(?:[^.`()\s]+\.)*)(?<M>[^.()`\s]+)(?<E>.*)$");
        private string identifier;

        /// <summary>
        /// Initializes a new instance of the <see cref="ID"/> class for the 
        /// specified cref <see cref="XAttribute"/>.
        /// </summary>
        /// <param name="cref">The cref <see cref="XAttribute"/>.</param>
        public ID(XAttribute cref)
        {
            if (cref == null) throw new ArgumentNullException("cref");

            identifier = cref.Value;
            Parse(identifier);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ID"/> class for the 
        /// specified ID string.
        /// </summary>
        /// <param name="identifier">A string containing the ID string.</param>
        public ID(string identifier)
        {
            if (identifier == null) throw new ArgumentNullException("identifier");

            this.identifier = identifier;
            Parse(identifier);
        }

        /// <summary>
        /// Represents the kinds of members that can be identified.
        /// </summary>
        public enum MemberType
        {
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
        /// Gets a value indicating the kind of member being identified.
        /// </summary>
        public MemberType Prefix { get; protected set; }

        /// <summary>
        /// Gets or sets the fully qualified name of the member being 
        /// identified, starting at the root of the namespace. Periods in names
        /// have been replaced with '#'.
        /// </summary>
        public string FullName { get; protected set; }

        /// <summary>
        /// Gets a string that contains a representation of the ID string that
        /// can be used for display purposes, e.g. as the text of a hyperlink.
        /// </summary>
        public string DisplayName { get; protected set; }

        /// <summary>
        /// Gets any additional unparsed information present in the ID string.
        /// </summary>
        public string Arguments { get; protected set; }

        /// <summary>
        /// Converts the specified <see cref="ID"/> string to a <see 
        /// cref="String"/>.
        /// </summary>
        /// <param name="id">The ID string to convert.</param>
        /// <returns>The converted <see cref="String"/>.</returns>
        public static implicit operator string(ID id)
        {
            return id.ToString();
        }

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
        /// Returns a string representation of the <see cref="ID"/> string.
        /// </summary>
        /// <returns>A string containing the fully qualified ID string.</returns>
        public override string ToString()
        {
            return identifier;
        }

        /// <summary>
        /// Parses the specified ID string.
        /// </summary>
        /// <param name="identifier">The ID string to parse.</param>
        protected void Parse(string identifier)
        {
            var match = parser.Match(identifier);
            if (match.Success)
            {
                var prefix = match.Groups["P"].Value[0];
                var ns = match.Groups["N"].Value;
                var member = match.Groups["M"].Value;
                var extra = match.Groups["E"].Value;

                Prefix = (MemberType)prefix;
                FullName = ns + member;
                DisplayName = member;
                Arguments = extra;
            }
        }
    }
}
