using System.Xml.Linq;

namespace Wakawaka.Documentation
{
    /// <summary>
    /// Represents the XML documentation for a property.
    /// </summary>
    public class Property : Member
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Property"/> class with
        /// the specified ID string and XML documentation.
        /// </summary>
        /// <param name="id">The ID string that idenfities the property.</param>
        /// <param name="member">The <see cref="XElement"/> object that 
        /// contains the XML documenation for the property.</param>
        public Property(string id, XElement member)
            : base(id, member) { }

        /// <summary>
        /// Returns a string representation of the <see cref="Property"/>.
        /// </summary>
        /// <returns>A string containing the name of the property.</returns>
        public override string ToString()
        {
            return string.Format("{0}.{1} Property", ID.ClassName, ID.Name);
        }
    }
}
