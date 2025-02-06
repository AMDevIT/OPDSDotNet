namespace AMDevIT.OPDSDotNet.Client.Core
{
    public class CatalogEntry(string? title, 
                              string? author, 
                              string? summary, 
                              string? link)
    {
        #region Properties

        public string? Title { get; set; } = title;
        public string? Author { get; set; } = author;
        public string? Summary { get; set; } = summary;
        public string? Link { get; set; } = link;

        #endregion
    }

}
