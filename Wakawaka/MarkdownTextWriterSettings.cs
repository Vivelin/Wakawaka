namespace Wakawaka
{
    /// <summary>
    /// Specifies a set of options that control the formatting of a <see 
    /// cref="MarkdownTextWriter"/> object.
    /// </summary>
    public sealed class MarkdownTextWriterSettings
    {
        /// <summary>
        /// A <see cref="MarkdownTextWriterSettings"/> object with all options
        /// set to their default values.
        /// </summary>
        public static readonly MarkdownTextWriterSettings Defaults = 
            new MarkdownTextWriterSettings();

        /// <summary>
        /// Initializes a new instance of the <see 
        /// cref="MarkdownTextWriterSettings"/> class.
        /// </summary>
        public MarkdownTextWriterSettings()
        {

        }
    }
}
