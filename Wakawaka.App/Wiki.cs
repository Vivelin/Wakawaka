using System;
using LibGit2Sharp;

namespace Wakawaka.App
{
    /// <summary>
    /// Represents a project's GitHub Wiki.
    /// </summary>
    internal class Wiki
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Wiki"/> class for the
        /// specified URI and working directory without authentication.
        /// </summary>
        /// <param name="uri">
        /// The URI to the git repository of the Wiki, e.g.
        /// https://github.com/horsedrowner/Wakawaka.wiki.git.
        /// </param>
        /// <param name="workDir">
        /// The path to the directory where a local working copy of the wiki
        /// repository will be stored.
        /// </param>
        public Wiki(string uri, string workDir)
            : this(uri, workDir, WikiSettings.Defaults)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Wiki"/> class for the
        /// specified URI, working directory and settings.
        /// </summary>
        /// <param name="uri">
        /// The URI to the git repository of the Wiki, e.g.
        /// https://github.com/horsedrowner/Wakawaka.wiki.git.
        /// </param>
        /// <param name="workDir">
        /// The path to the directory where a local working copy of the wiki
        /// repository will be stored.
        /// </param>
        /// <param name="settings">Specifies authentication settings.</param>
        public Wiki(string uri, string workDir, WikiSettings settings)
        {
            Uri = uri;
            WorkingDirectory = workDir;
            Settings = settings;
        }

        /// <summary>
        /// Gets the authentication settings for this wiki.
        /// </summary>
        public WikiSettings Settings { get; }

        /// <summary>
        /// Gets the URI to the GitHub Wiki's git repository.
        /// </summary>
        public string Uri { get; }

        /// <summary>
        /// Gets or sets the path to the directory where a local working copy of
        /// the wiki repository will be stored.
        /// </summary>
        public string WorkingDirectory { get; set; }

        /// <summary>
        /// Publishes the specified project's documentation to the wiki.
        /// </summary>
        /// <param name="project">
        /// The <see cref="Project"/> whose documentation will be published.
        /// </param>
        public void Publish(Project project)
        {
            var path = Repository.Clone(Uri, WorkingDirectory, Settings.GetOptions());
            using (var repo = new Repository(path))
            {
                var pages = project.GenerateDocumentation(WorkingDirectory);
                repo.Stage(pages);

                var message = "Wakawaka wiki update";
            }
        }
    }
}
