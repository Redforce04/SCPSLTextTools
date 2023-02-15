using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMiro.Framework
{
    public static class Objects
    {
        public static Dictionary<objects, Type> ObjectsDictionary = new Dictionary<objects, Type>()
        {
            { objects.Board, typeof(Board) },
            { objects.BoardMember, typeof(BoardMember) },
            { objects.AppCardItemData, typeof(AppCardItemData) },
            { objects.CardItemData, typeof(CardItemData) },
            { objects.DocumentItemData, typeof(DocumentItemData) },
            { objects.EmbedItemData, typeof(EmbedItemData) },
            { objects.FrameItemData, typeof(FrameItemData) },
            { objects.ImageItemData, typeof(ImageItemData) },
            { objects.PreviewItemData, typeof(PreviewItemData) },
            { objects.ShapeItemData, typeof(ShapeItemData) },
            { objects.StickyNoteItemData, typeof(StickyNoteItemData) },
            { objects.TextItemData, typeof(TextItemData) },
            { objects.Geometry, typeof(Geometry) },
            { objects.Picture, typeof(Picture) },
            { objects.Position, typeof(Position) },
            { objects.Team, typeof(Team) },
            { objects.User, typeof(User) },
            { objects.Tag, typeof(Tag) },
            { objects.Policy, typeof(Policy) },
            { objects.PermissionsPolicy, typeof(PermissionsPolicy) },
            { objects.SharingPolicy, typeof(SharingPolicy) },

        };
        public enum objects
        {
            Board,
            BoardMember,
            AppCardItemData,
            CardItemData,
            DocumentItemData,
            EmbedItemData,
            FrameItemData,
            ImageItemData,
            PreviewItemData,
            ShapeItemData,
            StickyNoteItemData,
            TextItemData,
            Geometry,
            Picture,
            Position,
            Team,
            User,
            Tag,
            Policy,
            PermissionsPolicy,
            SharingPolicy,
            Authorization
        }
    }
}
