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
        public string Email { get; set; }

        public User(string username, string password, string email)
        {
            Username = username;
            Password = password;
            Email = email;
        }
    }

    public class DescriptionUser
    {
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }
        public string Username { get; set; }
        public string CompleteName { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public DateTime Birthday { get; set; }
        public bool Premium { get; set; }

        public DescriptionUser(string username, string completeName, string gender, string phone, string country, DateTime birthday, bool premium)
        {
            Username = username;
            CompleteName = completeName;
            Gender = gender;
            Phone = phone;
            Country = country;
            Birthday = birthday;
            Premium = premium;
        }
    }

    public class Model
    {
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }
        public string NameProject { get; set; }
        public string Username { get; set; }
        public string ProjectPath { get; set; }
        public string ModelPath { get; set; }
        public string DataSetPath { get; set; }
        public string FileTsvPath { get; set; }
    }

    public class Genders
    {
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }
        public string Name { get; set; }

        public Genders(string name)
        {
            Name = name;
        }
    }

    public class Countries
    {
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Acronym { get; set; }

        public Countries(string name, string acronym)
        {
            Name = name;
            Acronym = acronym;
        }
    }
}