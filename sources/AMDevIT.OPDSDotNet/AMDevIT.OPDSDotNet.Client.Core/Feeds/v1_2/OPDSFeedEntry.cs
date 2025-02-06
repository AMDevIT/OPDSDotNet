using System.Xml.Serialization;

namespace AMDevIT.OPDSDotNet.Client.Core.Feeds.v1_2
{
    public class OPDSFeedEntry(string? id,
                           string? title,
                           string? description, 
                           DateTimeOffset? updated,                            
                           string? content,
                           OPDSFeedLink[]? links)
        : AtomFeedElementBase(id, title, description, updated)
    {
        #region Properties
      
        public string? Author
        {
            get;
            set;
        }

        [XmlElement("link")]
        public OPDSFeedLink[]? Links
        {
            get;
            set;
        } = links;

        [XmlElement("content")]
        public string? Content
        {
            get;
            set;
        } = content;

        #endregion

        #region .ctor

        public OPDSFeedEntry()
            : this(null, null, null, null, null, [])
        {
        }

        #endregion
    }
}
