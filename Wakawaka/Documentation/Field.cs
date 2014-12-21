using System;
using System.Xml.Linq;

namespace Wakawaka.Documentation
{
    /// <summary>
    /// Represents the XML documentation for a field.
    /// </summary>
    public class Field : Member
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Field"/> class with
        /// the specified ID string and XML documentation.
        /// </summary>
        /// <param name="id">The ID string that idenfities the field.</param>
        /// <param name="member">
        /// The <see cref="XElement"/> object that contains the XML 
        /// documentation for the field.
        /// </param>
        public Field(string id, XElement member)
            : base(id, member) { }

        /// <summary>
        /// Returns a string representation of the <see cref="Field"/>.
        /// </summary>
        /// <returns>A string containing the name of the field.</returns>
        public override string ToString()
        {
            return String.Format("{0}.{1} Field", ID.ClassName, ID.Name);
        }
    }
}
