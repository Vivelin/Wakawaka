using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Wakawaka.Documentation.Tags;

namespace Wakawaka.Documentation
{
    /// <summary>
    /// Represents the XML documentation for a method.
    /// </summary>
    public class MethodDocumentation : MemberDocumentation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MethodDocumentation"/>
        /// class with the specified ID string and XML documentation.
        /// </summary>
        /// <param name="id">The ID string that idenfities the method.</param>
        /// <param name="member">
        /// The <see cref="XElement"/> object that contains the XML documenation
        /// for the method.
        /// </param>
        public MethodDocumentation(string id, XElement member)
            : base(id, member)
        {
            if (member.Element("returns") != null)
                ReturnValue = Tag.Create(member.Element("returns"));

            Parameters = member.Elements("param").Select(x => Tag.Create(x));
            Exceptions = member.Elements("exception").Select(x => Tag.Create(x));
        }

        /// <summary>
        /// Gets the collection of tags that are used to describe which
        /// exceptions can be thrown.
        /// </summary>
        public IEnumerable<Tag> Exceptions { get; }

        /// <summary>
        /// Gets a value indicating whether the method is a constructor.
        /// </summary>
        public bool IsConstructor
        {
            get { return ID.Name == ".ctor"; }
        }

        /// <summary>
        /// Gets the collection of tags that are used to describe the parameters
        /// for the method.
        /// </summary>
        public IEnumerable<Tag> Parameters { get; }

        /// <summary>
        /// Gets the tag that is used to describe the return value of the
        /// method.
        /// </summary>
        public Tag ReturnValue { get; }

        /// <summary>
        /// Returns a string representation of the <see
        /// cref="MethodDocumentation"/>.
        /// </summary>
        /// <returns>A string containing the name of the method.</returns>
        public override string ToString()
        {
            if (IsConstructor)
                return String.Format("{0} Constructor", ID.ClassName);
            return String.Format("{0}.{1} Method", ID.ClassName, ID.Name);
        }

        /// <summary>
        /// Renders a Markdown representation of the <see
        /// cref="MethodDocumentation"/>'s name, summary and syntax.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="MarkdownTextWriter"/> object to write to.
        /// </param>
        protected override void RenderHeader(MarkdownTextWriter writer)
        {
            base.RenderHeader(writer);

            if (Parameters != null && Parameters.Count() > 0)
            {
                writer.WriteHeading("Parameters", 2);
                foreach (ParamTag param in Parameters)
                {
                    writer.WriteEmphasis(param.Name);
                    writer.WriteLineBreak();
                    param.Render(writer);
                    writer.WriteLine();
                }
            }

            if (ReturnValue != null)
            {
                writer.WriteHeading("Return Value", 2);
                ReturnValue.Render(writer);
                writer.WriteLine();
            }

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
    }
}
