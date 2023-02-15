using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMiro.Framework
{
    public class PreviewItemData
    {
        /// <summary>
        /// A text extract providing a short description of the content, as returned by the content provider.
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// The title of the preview content, as returned by the content provider.
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// A valid URL pointing to a web page.
        /// Possible transport protocols:
        /// - HTTP,
        /// - HTTPS.
        /// </summary>
        public Uri url { get; set; }

    }
}
