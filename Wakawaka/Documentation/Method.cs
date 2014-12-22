﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Wakawaka.Documentation.Tags;

namespace Wakawaka.Documentation
{
    /// <summary>
    /// Represents the XML documentation for a method.
    /// </summary>
    public class Method : Member
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Method"/> class with
        /// the specified ID string and XML documentation.
        /// </summary>
        /// <param name="id">The ID string that idenfities the method.</param>
        /// <param name="member">
        /// The <see cref="XElement"/> object that contains the XML 
        /// documenation for the method.
        /// </param>
        public Method(string id, XElement member)
            : base(id, member)
        {
            if (member.Element("returns") != null)
                ReturnValue = Tag.Create(member.Element("returns"));

            var parameters = member.Elements("param");
            Parameters = parameters.Select(x => Tag.Create(x));
        }

        /// <summary>
        /// Gets the tag that is used to describe the return value of the
        /// method.
        /// </summary>
        public Tag ReturnValue { get; }

        /// <summary>
        /// Gets the collection of tags that are used to describe the 
        /// parameters for the method.
        /// </summary>
        public IEnumerable<Tag> Parameters { get; }

        /// <summary>
        /// Gets a value indicating whether the method is a constructor.
        /// </summary>
        public bool IsConstructor
        {
            get { return ID.Name == ".ctor"; }
        }

        /// <summary>
        /// Renders a Markdown representation of the <see cref="Method"/>.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="MarkdownTextWriter"/> object to write to.
        /// </param>
        public override void Render(MarkdownTextWriter writer)
        {
            base.Render(writer);

            if (Parameters != null && Parameters.Count() > 0)
            {
                writer.WriteHeading("Parameters", 3);
                foreach (ParamTag param in Parameters)
                {
                    writer.WriteHeading(param.Name, 4);
                    param.Render(writer);
                }
            }

            if (ReturnValue != null)
            {
                writer.WriteHeading("Return Value", 3);
                ReturnValue.Render(writer);
                writer.WriteLine();
            }

            if (Remarks != null)
            {
                writer.WriteHeading("Remarks", 2);
                Remarks.Render(writer);
                writer.WriteLine();
            }
            if (Example != null)
            {
                writer.WriteHeading("Examples", 2);
                Example.Render(writer);
                writer.WriteLine();
            }

            // See Also
        }

        /// <summary>
        /// Returns a string representation of the <see cref="Method"/>.
        /// </summary>
        /// <returns>A string containing the name of the method.</returns>
        public override string ToString()
        {
            if (IsConstructor)
                return String.Format("{0} Constructor", ID.ClassName);
            return String.Format("{0}.{1} Method", ID.ClassName, ID.Name);
        }
    }
}
