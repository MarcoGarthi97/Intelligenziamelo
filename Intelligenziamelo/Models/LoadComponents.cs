using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.VisualBasic.ApplicationServices;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver;
using Intelligenziamelo.Properties;

namespace Intelligenziamelo.Models
{
    public class LoadComponents
    {
        string pathGenders = @"C:\Users\marco\Downloads\genders.csv";
        string pathCountries = @"C:\Users\marco\Downloads\countries.csv";
        private static string connectionString = File.ReadAllText(Settings.Default.PathAtlasCredentials);

        public async Task<bool> LoadAllComponents()
        {
            //LoadGender();
            //LoadCountries();
            return true;
        }

        public async Task<bool> LoadGender()
        {
            List<string> file = File.ReadAllLines(pathGenders).ToList();
            List<Genders> gendersList = new List<Genders>();
            foreach(var gender in file)
                gendersList.Add(new Genders(gender.ToLower()));

            var settings = MongoClientSettings.FromConnectionString(connectionString);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var mongoClient = new MongoClient(settings);

            var intelligenziameloDB = mongoClient.GetDatabase("IntelligenziameloDB");

           IMongoCollection<Genders> genders = intelligenziameloDB.GetCollection<Genders>("Genders");
            genders.InsertMany(gendersList);

            return true;
        }

        public async Task<bool> LoadCountries()
        {
            List<string> file = File.ReadAllLines(pathCountries).ToList();
            List<Countries> countriesList = new List<Countries>();
            foreach(var country in file) 
                countriesList.Add(new Countries(country.Split(',')[0].ToLower(), country.Split(',')[1].ToLower()));

            var settings = MongoClientSettings.FromConnectionString(connectionString);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var mongoClient = new MongoClient(settings);

            var intelligenziameloDB = mongoClient.GetDatabase("IntelligenziameloDB");

            IMongoCollection<Countries> countries = intelligenziameloDB.GetCollection<Countries>("Countries");
            countries.InsertMany(countriesList);

            return true;
        }
    }
}