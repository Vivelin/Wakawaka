using System.Xml.Linq;

namespace Wakawaka.Documentation
{
    /// <summary>
    /// Represents the XML documentation for an event.
    /// </summary>
    public class Event : Member
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Event"/> class with
        /// the specified ID string and XML documentation.
        /// </summary>
        /// <param name="id">The ID string that idenfities the event.</param>
        /// <param name="member">The <see cref="XElement"/> object that 
        /// contains the XML documenation for the event.</param>
        public Event(string id, XElement member)
            : base(id, member) { }

        /// <summary>
        /// Returns a string representation of the <see cref="Event"/>.
        /// </summary>
        /// <returns>A string containing the name of the event.</returns>
        public override string ToString()
        {
            return string.Format("{0}.{1} Event", ID.ClassName, ID.Name);
        }
    }
}
