using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Wakawaka.Tests
{
    [TestClass]
    public class MarkdownTests
    {
        /// <remarks>
        /// http://spec.commonmark.org/0.7/#code-span
        /// </remarks>
        [TestMethod]
        public void CodeSpan_GeneratesBackticks()
        {
            var result = Markdown.CodeSpan("foo");
            Assert.AreEqual("`foo`", result,
                "Code span should be encloused with single backticks.");
        }

        /// <remarks>
        /// http://spec.commonmark.org/0.7/#code-span
        /// </remarks>
        [TestMethod]
        public void CodeSpan_WithBackticks_GeneratesDoubleBackticks()
        {
            var result = Markdown.CodeSpan(" foo ` bar ");
            Assert.AreEqual("`` foo ` bar ``", result,
                "Code span containing ` should be enclosed with double backticks.");
        }

        /// <summary>
        /// http://spec.commonmark.org/0.7/#example-291
        /// </summary>
        [TestMethod]
        public void Emphasis_WithAsterisk_IsEscaped()
        {
            var result = Markdown.Emphasis("here is a *");
            Assert.AreEqual("*here is a \\**", result,
                "Emphasis should escape * with a backslash.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Heading_LessThanOne_ShouldThrow()
        {
            Markdown.Heading("Heading 0", 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Heading_HigherThanSix_ShouldThrow()
        {
            Markdown.Heading("Heading 7", 7);
        }
    }
}
