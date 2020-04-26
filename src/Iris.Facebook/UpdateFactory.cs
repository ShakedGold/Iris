using System;
using System.Collections.Generic;
using System.Text;
using Iris.Api;

namespace Iris.Facebook
{
    internal static class UpdateFactory
    {
        public static Update ToUpdate(this Post post, User author)
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
                        imageUrl.ToMedia()
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
        
        private static Media ToMedia(this string imageUrl)
        {
            return new Media(imageUrl, MediaType.Photo);
        }

        private static string GetFormattedMessage(Post post, User author)
        {
            string verb = author.Gender == Gender.Male ? "פרסם" : "פרסמה";
            const string postWord = "פוסט";
            
            return $"{author.Name} {verb} {postWord}:\n \n \n{post.Text}\n \n{post.PostUrl}";
        }
    }
}