using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intelligenziamelo.Models
{
    public class User
    {
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class DescriptionUser
    {
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool Premium { get; set; }
    }
}