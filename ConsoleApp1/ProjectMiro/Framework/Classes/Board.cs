using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace ProjectMiro.Framework
{
    public class Board
    {
        public static Board GetBoard()
        {
            return null;
        }
        private Board()
        {

        }
        public Board(string name, string description, string teamId, Policy policy)
        {

        }
        /// <summary>
        /// Name for the board.
        /// </summary>
        public string name { get; private set; } = "";
        /// <summary>
        /// Description of the board.
        /// </summary>
        public string description { get; private set; } = "";
        /// <summary>
        /// Unique identifier (ID) of the team where the board must be placed.
        /// </summary>
        public string teamId { get; private set; } = "";
        /// <summary>
        ///Defines the permissions policies and sharing policies for the board. For more information, see <seealso cref="policy"/>.
        /// </summary>
        public Policy policy { get; private set; }
        /// <summary>
        /// Contains information about the team with which the board is associated. For more information, see <seealso cref="team"/>.
        /// </summary>
        public Team team { get; private set; }

        /// <summary>
        /// Contains information about the cover picture of the board.
        /// </summary>
        public Picture picture { get; private set; }

        /// <summary>
        /// URL to view the board.
        /// </summary>
        public string viewLink { get; private set; }

        /// <summary>
        /// Date and time when the board was created. Format: UTC, adheres to ISO 8601, includes a trailing Z offset.
        /// </summary>
        public DateTime createdAt { get; private set; }

        public static void CreateBoard(string description, string name, Policy policy, string teamId)
        {
            /*Dictionary<string, string> args = new Dictionary<string, string>()
            {
                { "description", description },
                { "name", name },
                { "policy", JsonSerialize.ToJson<Policy>(policy) },
                { "teamId", teamId }
            };
            API.Api.MakeAPICall("boards", JsonSerialize.ToJson<Dictionary<string,string>>(args), Method.Post);
            */
        }

        public static void GetBoards()
        {

        }

        public static void CopyBoard()
        {

        }

        public static void GetSpecificBoard()
        {

        }
        public static void UpdateBoard()
        {

        }

        public static void DeleteBoard()
        {

        }
    
        public enum BoardAction
        {
            CreateBoard,
            GetBoards,
            CopyBoard,
            GetSpecificBoard,
            UpdateBoard,
            DeleteBoard
        }
    }

}
