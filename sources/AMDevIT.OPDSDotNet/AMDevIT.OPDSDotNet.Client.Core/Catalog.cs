using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDevIT.OPDSDotNet.Client.Core
{
    public class Catalog(string? title, 
                         string? description, 
                         IEnumerable<CatalogEntry> entries, 
                         string? nextPage, 
                         string? previousPage)
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

        public IEnumerable<CatalogEntry> Entries 
        { 
            get; 
            set; 
        } = entries;

        public string? NextPage 
        { 
            get; 
            set; 
        } = nextPage;

        public string? PreviousPage 
        { 
            get; 
            set; 
        } = previousPage;

        #endregion
    }

}
