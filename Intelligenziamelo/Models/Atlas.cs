using Intelligenziamelo.Controllers;
using Intelligenziamelo.Properties;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Intelligenziamelo.Models
{
    public class Atlas
    {
        private static string connectionString = File.ReadAllText(Settings.Default.PathAtlasCredentials);

        private async Task<IMongoDatabase> GetDatabase()
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var mongoClient = new MongoClient(settings);

            return mongoClient.GetDatabase("IntelligenziameloDB");
        }

        public async Task<bool> Login(string username, string password)
        {
            try
            {
                var intelligenziameloDB = GetDatabase().Result;
                IMongoCollection<User> users = intelligenziameloDB.GetCollection<User>("Users");

                var user = users.Find(x => (x.Username == username.ToLower() || x.Email == username.ToLower()) && x.Password == password).FirstOrDefault();
                if(user != null)
                {
                    IMongoCollection<DescriptionUser> descriptionUsers = intelligenziameloDB.GetCollection<DescriptionUser>("DescriptionUsers");

                    var description = descriptionUsers.Find(x => x.Username == username.ToLower()).FirstOrDefault();
                    if(description != null)
                    {
                        HomeController.userModel = new UserModel();

                        HomeController.userModel.Login = true;
                        HomeController.userModel.User = user;
                        HomeController.userModel.DescriptionUser = description;

                        IMongoCollection<Model> models = intelligenziameloDB.GetCollection<Model>("Models");

                        var model = models.Find(x => x.Username == username.ToLower()).FirstOrDefault();
                        if(model != null)
                        {
                            HomeController.userModel.Model = model;
                        }
                    }                    

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> InsertModel(Model modelToInsert)
        {  
            try
            {
                IMongoDatabase intelligenziameloDB = GetDatabase().Result;
                IMongoCollection<Model> model = intelligenziameloDB.GetCollection<Model>("Models");

                model.InsertOne(modelToInsert);
            }
            catch (Exception ex)
            {

            }

            return true;
        }

        public async Task<bool> InsertUsers(User user, DescriptionUser descriptionUser)
        {
            try
            {
                var intelligenziameloDB = GetDatabase().Result;

                IMongoCollection<User> users = intelligenziameloDB.GetCollection<User>("Users");
                users.InsertOne(user);

                IMongoCollection<DescriptionUser> descriptionUsers = intelligenziameloDB.GetCollection<DescriptionUser>("DescriptionUsers");
                descriptionUsers.InsertOne(descriptionUser);

                HomeController.userModel = new UserModel();
                HomeController.userModel.Login = true;
                HomeController.userModel.User = user;
                HomeController.userModel.DescriptionUser = descriptionUser;
            }
            catch(Exception ex)
            {

            }

            return true;
        }

        public async Task<bool> CheckUsername(string username)
        {
            try
            {
                var intelligenziameloDB = GetDatabase().Result;

                IMongoCollection<User> users = intelligenziameloDB.GetCollection<User>("Users");

                var user = users.Find(x => x.Username == username.ToLower()).FirstOrDefault();
                if (user != null) return false;
                else return true;
            }
            catch(Exception ex) 
            {
                return false;
            }            
        }

        public async Task<bool> CheckEmail(string email)
        {
            try
            {
                var intelligenziameloDB = GetDatabase().Result;

                IMongoCollection<User> users = intelligenziameloDB.GetCollection<User>("Users");

                var user = users.Find(x => x.Email == email.ToLower()).FirstOrDefault();
                if (user != null) return false;
                else return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<string>> GetGender()
        {
            List<string> gendersList = new List<string>();

            try
            {
                var intelligenziameloDB = GetDatabase().Result;

                IMongoCollection<Genders> gendersAtlas = intelligenziameloDB.GetCollection<Genders>("Genders");
                var genders = gendersAtlas.Find(x => x.Name != "").ToList();

                foreach (Genders gender in genders)
                    gendersList.Add(gender.Name);
            }
            catch(Exception ex)
            {

            }

            return gendersList;
        }
    }
}