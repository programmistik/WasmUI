using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WasmUI.Shared
{
    [BsonIgnoreExtraElements]
    public class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }

        public string UniqueId { get; set; }

        public string ProfileId { get; set; }

        public string Image { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public List<string> ViewsProfileId { get; set; }
        public List<string> LikesProfileId { get; set; }
        public List<string> Hashtags { get; set; }

        public string getPath()
        {
            if (Image == null)
                return @"https://i.stack.imgur.com/y9DpT.jpg";
            return Image;
        }
        public string getMinPath()
        {
            if (Image == null)
                return @"https://i.stack.imgur.com/y9DpT.jpg";
            string newstr = Image.Replace("posts", "resizedposts");

            return newstr;
        }
    }

}
