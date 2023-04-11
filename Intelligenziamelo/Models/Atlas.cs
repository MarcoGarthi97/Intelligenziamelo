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
        private string connectionString = File.ReadAllText(Settings.Default.PathAtlasCredentials);

        public async Task<bool> Login(string username, string password)
        {
            try
            {
                var settings = MongoClientSettings.FromConnectionString(connectionString);
                settings.ServerApi = new ServerApi(ServerApiVersion.V1);
                var mongoClient = new MongoClient(settings);

                var usersDB = mongoClient.GetDatabase("IntelligenziameloDB");
                IMongoCollection<User> users = usersDB.GetCollection<User>("Users");

                var results = users.Find(x => x.Username == username.ToLower()).ToList();
                foreach(var user in results)
                {
                    if(user.Password == password)
                    {
                        HomeController.userModel = new UserModel();
                        HomeController.userModel.Username = username;
                        HomeController.userModel.Password = password;
                        HomeController.userModel.Login = true;

                        return true;
                    }
                        
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}