using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WasmUI.Shared
{
    [BsonIgnoreExtraElements]
    public class ProfileClass
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }

        public string IdentityId { get; set; }
        public string AppUserId { get; set; }
        public string Name { get; set; }
        public string Avatara { get; set; }
        public string From { get; set; }
        public string Whatsapp { get; set; }
        public string Skype { get; set; }

        public List<string> Friends { get; set; }
        public List<string> BlockedUsers { get; set; }

        public List<LikedPost> LikedPosts { get; set; }

        public bool isBlocked(string AppUserId)
        {
            if (BlockedUsers.Where(u => u.Equals(AppUserId)).Count() > 0)
                return true;
            return false;
        }

        public bool isFriend(string AppUserId)
        {
            if (Friends.Where(u => u.Equals(AppUserId)).Count() > 0)
                return true;
            return false;
        }
    }

    public class LikedPost
    {
        public string PostId { get; set; }
        public DateTime Date { get; set; }
    }

    public class LikedPostFull
    {
        public Post Post { get; set; }
        public DateTime Date { get; set; }
    }
}
