using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Xml.Linq;

namespace AMDev.IT.OPDSDotNet.Client.Core
{
    public class OPDSClient(HttpClient netClient, ILogger? logger)
    {
        #region Fields

        private readonly HttpClient netClient = netClient;
        private readonly ILogger? logger = logger;

        #endregion

        #region Properties

        protected HttpClient NetClient => this.netClient;        
        protected ILogger? Logger => this.logger;

        #endregion

        #region .ctor

        public OPDSClient()
            : this(new HttpClient(), null)
        {
        }

        #endregion

        #region Methods

        public async Task<OPDSFeed?> GetFeedAsync(string url, CancellationToken cancellationToken = default)
        {
            try
            {
                XDocument document;
                HttpResponseMessage response = await this.netClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync(cancellationToken);
                document = XDocument.Parse(content);

                return await ParseFeedAsync(document, cancellationToken);
            }
            catch (Exception exc)
            {
                this.logger?.LogError(exc, "Error during feed download.");
                return null;
            }
        }

        protected async Task<OPDSFeed> ParseFeedAsync(XDocument xmlDocument, CancellationToken cancellationToken = default)
        {
            XNamespace ns = xmlDocument.Root!.Name.Namespace;
            string? feedTitle;
            string? feedDescription;
            ConcurrentBag<OPDSEntry> entriesList = [];
            OPDSFeed feed;

            // Read the feed information
            feedTitle = xmlDocument.Root.Element(ns + "title")?.Value;
            feedDescription = xmlDocument.Root.Element(ns + "subtitle")?.Value;

            // Read the entries
            IEnumerable<XElement> feedElements = xmlDocument.Root.Elements(ns + "entry").ToArray();

            await Parallel.ForEachAsync(feedElements, 
                                        cancellationToken, 
                                        (entryElement, cancellationToken) =>
            {
                OPDSEntry entry = new OPDSEntry
                {
                    Title = entryElement.Element(ns + "title")?.Value,
                    Author = entryElement.Element(ns + "author")?.Element(ns + "name")?.Value,
                    Link = entryElement.Element(ns + "link")?.Attribute("href")?.Value,
                    Summary = entryElement.Element(ns + "summary")?.Value
                };

                entriesList.Add(entry);
                return ValueTask.CompletedTask;
            });           

            feed = new OPDSFeed(feedTitle, feedDescription, [.. entriesList]);
            return feed;
        }

        #endregion
    }
}
