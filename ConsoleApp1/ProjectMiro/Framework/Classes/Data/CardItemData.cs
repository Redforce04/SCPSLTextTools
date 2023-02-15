using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMiro.Framework
{
    public class CardItemData
    {
        /// <summary>
        /// A short text header for the card. Besides plain text, title also supports the following HTML tags:
        /// - p,
        /// - a,
        /// - br.
        /// Unsupported HTML tags are automatically stripped.
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// A short text description to provide context and details about the card.
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
        /// The date when the task or activity described in the card is due to be completed.
        /// In the GUI, users can select the due date from a calendar.
        /// Format: UTC, adheres to ISO 8601, includes a trailing Z offset.
        /// </summary>
        public DateTime dueDate { get; set; }

        /// <summary>
        /// Unique user identifier.
        /// In the GUI, the user ID is mapped to the name of the user who is assigned as the owner of the task or activity described in the card.
        /// The identifier is numeric, and it is automatically assigned to a user when they first sign up.
        /// </summary>
        public string assigneeId { get; set; }

        public enum Actions
        {
            CreateCardItem,
            GetCardItem,
            UpdateCardItem,
            DeleteCardItem
        }
    }
}
