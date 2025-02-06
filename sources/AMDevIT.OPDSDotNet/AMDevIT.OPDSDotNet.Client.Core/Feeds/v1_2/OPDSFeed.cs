using System.Xml.Serialization;

namespace AMDevIT.OPDSDotNet.Client.Core.Feeds.v1_2
{
    [XmlRoot("feed", Namespace = "http://www.w3.org/2005/Atom")]
    public class OPDSFeed(string? id,
                          string? title,
                          string? description,
                          DateTimeOffset? updated,
                          OPDSFeedLink[] links,
                          OPDSFeedEntry[] entries,
                          ODPSFeedAuthor? author)
        : AtomFeedElementBase(id, title, description, updated)
    {
        #region Properties

        [XmlElement("author")]
        public ODPSFeedAuthor? Author
        {
            get;
            set;
        } = author;

        [XmlElement("link")]
        public OPDSFeedLink[]? Links
        {
            get;
            set;
        } = links;

        [XmlElement("entry")]
        public OPDSFeedEntry[]? Entries
        {
            get;
            set;
        } = entries;

        #endregion

        #region .ctor

        public OPDSFeed()
            : this(null, null, null, DateTimeOffset.UtcNow, [], [], null)
        {
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return base.ToString() + 
                   $", Entries: {Entries?.Length ?? 0}, " +
                   $"Links: {Links?.Length ?? 0}";
        }

        #endregion
    }
}
