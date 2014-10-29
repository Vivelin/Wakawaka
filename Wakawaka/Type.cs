using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Wakawaka
{
    /// <summary>
    /// Represents the XML documentation for a type (e.g. class, interface, 
    /// structure, enum or delegate).
    /// </summary>
    public class Type : Member
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Type"/> class with the
        /// specified ID string and XML documentation.
        /// </summary>
        /// <param name="id">The ID string that idenfities the member.</param>
        /// <param name="member">The <see cref="XElement"/> object that 
        /// contains the XML documenation for the member.</param>
        public Type(string id, XElement member) : base(id, member)
        {
            
        }
    }
}
