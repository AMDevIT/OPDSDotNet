using System.Xml.Serialization;

namespace AMDevIT.OPDSDotNet.Client.Core.Feeds.v1_2
{
    public abstract class AtomFeedElementBase(string? id,
                                              string? title,
                                              string? description,
                                              DateTimeOffset? updated)
    {
        #region Properties

        [XmlElement("id")]
        public string? ID
        {
            get;
            set;
        } = id;

        [XmlElement("title")]
        public string? Title
        {
            get;
            set;
        } = title;

        [XmlElement("description")]
        public string? Description
        {
            get;
            set;
        } = description;

        [XmlElement("updated")]
        public string? UpdatedStringValue
        {
            get;
            set;
        } = updated?.ToString();

        [XmlIgnore]
        public DateTimeOffset? Updated
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(this.UpdatedStringValue))
                    return DateTimeOffset.Parse(this.UpdatedStringValue);
                else
                    return null;
            }
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return $"Title: {Title}, Description: {Description}";
        }

        #endregion
    }
}
