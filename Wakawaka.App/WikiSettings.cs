using System;
using LibGit2Sharp;

namespace Wakawaka.App
{
    /// <summary>
    /// Specifies authentication settings for editing a GitHub Wiki.
    /// </summary>
    internal class WikiSettings
    {
        /// <summary>
        /// Represents a <see cref="WikiSettings"/> object with all values set
        /// to their defaults.
        /// </summary>
        public static readonly WikiSettings Defaults = new WikiSettings();

        /// <summary>
        /// Initializes a new instance of the <see cref="WikiSettings"/> class.
        /// </summary>
        public WikiSettings()
        {
        }

        /// <summary>
        /// Gets or sets the email address to be displayed with commit messages.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the full name to be displayed with commit messages.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the password of the user to authenticate with.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the name of the user to authenticate with.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets a <see cref="CloneOptions"/> object to authenticate with, or
        /// <c>null</c> if no username has been set.
        /// </summary>
        public CloneOptions GetOptions()
        {
            if (UserName != null)
            {
                return new CloneOptions
                {
                    CredentialsProvider = (x, y, z) => new UsernamePasswordCredentials
                    {
                        Username = UserName,
                        Password = Password
                    }
                };
            }

            return null;
        }

        /// <summary>
        /// Returns a new <see cref="Signature"/> to sign commits with, using
        /// the current settings.
        /// </summary>
        /// <returns>A new <see cref="Signature"/> object.</returns>
        /// <exception cref="InvalidOperationException">
        /// <see cref="EmailAddress"/> is <c>null</c>.
        /// </exception>
        public Signature GetSignature()
        {
            if (EmailAddress == null)
                throw new InvalidOperationException();

            return new Signature(FullName ?? EmailAddress, EmailAddress,
                DateTimeOffset.Now);
        }
    }
}
