namespace ProjectMiro.Framework
{
    /// <summary>
    /// Contains information about the permissions policies for the board.
    /// </summary>
    public class PermissionsPolicy
    {
        /// <summary>
        /// Defines who can start or stop timer, voting, video chat, screen sharing, attention management.
        /// Others will only be able to join. To change the value for the collaborationToolsStartAccess parameter, contact Miro Customer Support.
        /// </summary>
        public CollaborationToolsStartAccess collaborationToolsStartAccess { get; private set; } =
            CollaborationToolsStartAccess.all_editors;

        /// <summary>
        /// Defines who can copy the board, copy objects, download images, and save the board as a template or PDF.
        /// </summary>
        public CopyAccess copyAccess { get; private set; } = CopyAccess.anyone;

        /// <summary>
        /// Defines who can change access and invite users to this board. To change the value for the sharingAccess parameter, contact Miro Customer Support.
        /// </summary>
        public SharingAccess sharingAccess { get; private set; } = SharingAccess.team_members_with_editing_rights;
    }
}