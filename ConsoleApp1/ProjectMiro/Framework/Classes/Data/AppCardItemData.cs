using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMiro.Framework
{
    public class AppCardItemData
    {
        /// <summary>
        /// A short text header for the app card. Besides plain text, title also supports the following HTML tags:
        /// - p,
        /// - a,
        /// - br.
        /// Unsupported HTML tags are automatically stripped.
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// A short text description to provide context and details about the app card.
        /// Besides plain text, description also supports the following HTML tags:
        /// - p,
        /// - a,
        /// - strong,
        /// - b,
        /// - em,
        /// - i,
        /// - u,
        /// - ol,
        /// - ul,
        /// - li,
        /// - br.
        /// Unsupported HTML tags are automatically stripped.
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// A read-only Boolean value that controls read and write access to the app card.
        /// - true: the app requesting access can read and write to the app card properties.
        /// - false: the app requesting access can only read the app card properties.
        /// Only the app that created the app card has read and write access to it.
        /// Other apps can only view the app card properties.
        /// </summary>
        public bool owned { get; set; }

        /// <summary>
        /// Each object in the array represents a custom preview field that you can define for the app card.
        /// The preview fields are displayed on the bottom half of the app card in the compact view.
        /// For each preview field, you can define:
        /// - The data value of the custom field.
        /// For example, you can use value to communicate status or completion information; or a due date; or a plain text string.
        /// - The color of the text.
        /// - A background color for the preview field.
        /// - An image for the icon.
        /// The shape of the icon: either round, or square.
        /// - A short text for the tooltip displayed when clicking or hovering over the preview field.
        /// </summary>
        public List<AppCardField> fields { get; set; } = new List<AppCardField>();

        public enum Actions
        {
            CreateAppCardItem,
            GetAppCardItem,
            UpdateAppCardItem,
            DeleteAppCardItem
        }

    }
    public class AppCardField
    {
        /// <summary>
        /// Hex value representing the color that fills the background area of the preview field, when it's displayed on the app card.
        /// Default: transparent(no fill color)
        /// </summary>
        public string fillColor { get; set; }

        /// <summary>
        /// Defines the <seealso cref="IconShape"/> of the icon on the preview field.
        /// </summary>
        public IconShape iconShape { get; set; }

        /// <summary>
        /// A valid URL pointing to an image available online.
        /// The transport protocol must be HTTPS.
        /// Possible image file formats:
        /// - JPG / JPEG,
        /// - PNG,
        /// - SVG.
        /// </summary>
        public Uri iconUrl { get; set; }

        /// <summary>
        /// Hex value representing the color of the text string assigned to value.
        /// Default: #1a1a1a (black)
        /// </summary>
        /// 
        public string textColor { get; set; }

        /// <summary>
        /// A short text displayed in a tooltip when clicking or hovering over the preview field.
        /// </summary>
        public string tooltip { get; set; }

        /// <summary>
        /// The actual data value of the custom field.
        /// It can be any type of information that you want to convey.
        /// For example, you can use value to communicate status or completion information; or a due date; or a plain text string.
        /// </summary>
        public string value { get; set; }

    }
}
