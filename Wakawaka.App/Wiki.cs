using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;

namespace Wakawaka.App
{
    /// <summary>
    /// Represents a project's GitHub Wiki.
    /// </summary>
    class Wiki
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Wiki"/> class for the
        /// specified URI.
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
        {
            Uri = uri;
            WorkingDirectory = workDir;
        }

        /// <summary>
        /// Gets the URI to the GitHub Wiki's git repository.
        /// </summary>
        public string Uri { get; }

        /// <summary>
        /// Gets or sets the path to the directory where a local working copy
        /// of the wiki repository will be stored.
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
            var path = Repository.Clone(Uri, WorkingDirectory);
            using (var repo = new Repository(path))
            {
                
            }
        }
    }
}
