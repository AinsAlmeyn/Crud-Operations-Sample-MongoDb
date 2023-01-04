using System;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Crud.Mongo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        
        [HttpGet("GetStudentByName")]
        public IActionResult GetStudent(string data1)
        {
            var collection = DbConnection.GetUserCollection();
            var filter = Builders<BsonDocument>.Filter.Eq("Name", data1);
            var studentData = collection.Find(filter).FirstOrDefault();
            return Ok(studentData);
        }


        [HttpGet("MyDatabaseList")]
        public IActionResult GetDatabases()
        {
            var databaseList = DbConnection.GetMongoClient().ListDatabases().ToList();
            foreach (var item in databaseList)
            {
                Console.Write(item);
            }
            return Ok(databaseList as List<BsonDocument>);
        }
    }

    public static class DbConnection
    {
        public static IMongoCollection<BsonDocument> GetUserCollection()
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb://localhost:27017/");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("FirstTry");
            var collection = database.GetCollection<BsonDocument>("User");
            return collection;
        }

        public static MongoClient GetMongoClient()
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb://localhost:27017/");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            return client;
        }
    }
}
