using System;
using System.Xml.Linq;

namespace Wakawaka
{
    /// <summary>
    /// Represents the XML documentation for a single member.
    /// </summary>
    public class Member
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Member"/> class with
        /// the specified ID string and XML documentation.
        /// </summary>
        /// <param name="id">The ID string that idenfities the member.</param>
        /// <param name="member">The <see cref="XElement"/> object that 
        /// contains the XML documenation for the member.</param>
        protected Member(string id, XElement member)
        {
            ID = id;
            Summary = member.Element("summary").ToMarkdown();
        }

        /// <summary>
        /// Gets the ID string that identifies the <see cref="Member"/>.
        /// </summary>
        public string ID { get; protected set; }

        /// <summary>
        /// Gets a string that is used to describe a member.
        /// </summary>
        public string Summary { get; protected set; }
        
        /// <summary>
        /// Creates a new <see cref="Member"/> object for the specified <see 
        /// cref="XElement"/>.
        /// </summary>
        /// <param name="element">The <see cref="XElement"/> object that 
        /// contains the documentation for which to create a new <see 
        /// cref="Member"/>.</param>
        /// <returns>A new <see cref="Member"/> object of the type 
        /// corresponding to the member described in <paramref name="element"/>.
        /// </returns>
        public static Member Create(XElement element)
        {
            var name = element.Attribute("name").Value;
            if (name == null)
                throw new ArgumentException("The specified element does not have a name attribute", "element");
            if (name.Length < 3)
                throw new ArgumentException("The length of the name attribute is too short", "element");

            var prefix = name[0];
            switch (prefix)
            {
                case 'T': return new Type(name, element);
                default: return new Member(name, element);
            }
        }

        /// <summary>
        /// Gets a string representation of the <see cref="Member"/>.
        /// </summary>
        /// <returns>A string containing the ID string.</returns>
        public override string ToString()
        {
            return ID;
        }
    }
}
