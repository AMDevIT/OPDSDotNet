using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AMDevIT.OPDSDotNet.Client.Core.Feeds.v1_2
{   
    public class ODPSFeedAuthor
    {
        #region Properties

        [XmlElement("name")]
        public string? Name
        { 
            get;
            set;
        }

        [XmlElement("uri")]
        public string? Uri
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return $"Name: {Name}, Uri: {Uri}";
        }

        #endregion
    }
}
