using System;
using System.Collections.Generic;
using System.Text;
using Updates.Api;

namespace Updates.Facebook
{
    internal static class UpdateFactory
    {
        public static Update ToUpdate(Post post, User author)
        {
            long id = post.Id;
            string message = post.Text;
            string formattedMessage = GetFormattedMessage(post, author);
            DateTime createdAt = post.Date;
            string url = post.PostUrl;
            
            string imageUrl = post.ImageUrl;
            IEnumerable<Media> media = 
                imageUrl != null
                ? new List<Media>
                    {
                        MediaFactory.ToMedia(imageUrl)
                    }
                : new List<Media>();

            return new Update(
                id,
                author,
                message,
                formattedMessage,
                createdAt,
                url,
                media);
        }

        private static string GetFormattedMessage(Post post, User author)
        {
            var builder = new StringBuilder(FormatHeader("ציוץ חדש פורסם כעת מאת"));
            builder.Append(
                GetPostText(post, author));

            builder.Append(
                "\n \n \n \n" +
                $"{post.PostUrl}");

            return builder.ToString();
        }

        private static string FormatHeader(string header)
            => $"{header}:\n";

        private static string GetPostText(Post post, User author) => $"{author.DisplayName} \n \n \n \"{post.Text}\"";
    }
}