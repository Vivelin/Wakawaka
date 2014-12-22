using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using Wakawaka.Documentation.Tags;

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
        /// <param name="member">
        /// The <see cref="XElement"/> object that contains the XML 
        /// documentation for the event.
        /// </param>
        public Event(string id, XElement member)
            : base(id, member)
        {
            Exceptions = member.Elements("exception").Select(x => Tag.Create(x));
        }

        /// <summary>
        /// Gets the collection of tags that are used to describe which 
        /// exceptions can be thrown.
        /// </summary>
        public IEnumerable<Tag> Exceptions { get; }

        /// <summary>
        /// Renders a Markdown-formatted representation of the <see 
        /// cref="Event"/>'s name and summary.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="MarkdownTextWriter"/> object to write to.
        /// </param>
        protected override void RenderHeader(MarkdownTextWriter writer)
        {
            base.RenderHeader(writer);

            if (Exceptions != null && Exceptions.Count() > 0)
            {
                writer.WriteHeading("Exceptions", 2);
                foreach (ExceptionTag exception in Exceptions)
                {
                    writer.Write('*');
                    exception.CRef.Render(writer);
                    writer.Write('*');
                    writer.WriteLineBreak();
                    exception.Render(writer);
                    writer.WriteLine();
                }
            }
        }

        /// <summary>
        /// Returns a string representation of the <see cref="Event"/>.
        /// </summary>
        /// <returns>A string containing the name of the event.</returns>
        public override string ToString()
        {
            return String.Format("{0}.{1} Event", ID.ClassName, ID.Name);
        }
    }
}
