namespace AMDev.IT.OPDSDotNet.Client.Core
{
    public class OPDSFeed(string? title, 
                          string? description, 
                          IEnumerable<OPDSEntry> entries)
    {
        #region Properties

        public string? Title
        {
            get;
            set;
        } = title;

        public string? Description
        {
            get;
            set;
        } = description;

        public IEnumerable<OPDSEntry> Entries
        {
            get;
            set;
        } = entries;

        #endregion

        #region .ctor

        public OPDSFeed()
            : this(null, null, [])
        {
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return $"Title: {Title}, Description: {Description}, Entries: {Entries.Count()}";
        }

        #endregion
    }
}
