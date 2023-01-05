using System;
using MongoDB.Bson;
using MongoDB.Driver;


Console.WriteLine("Yildiray Kocak");
#region Create a new document
// Insert Data in Database

//var collection = DbConnection.GetUserCollection();
//var document = new BsonDocument
//            {
//                { "student_id", 10000 },
//                { "scores", new BsonArray
//                    {
//                    new BsonDocument{ {"type", "exam"}, {"score", 88.12334193287023 } },
//                    new BsonDocument{ {"type", "quiz"}, {"score", 74.92381029342834 } },
//                    new BsonDocument{ {"type", "homework"}, {"score", 89.97929384290324 } },
//                    new BsonDocument{ {"type", "homework"}, {"score", 82.12931030513218 } }
//                    }
//                },
//                { "class_id", 480}
//            };
//collection.InsertOne(document);

#endregion

#region Read a document without Filter

//var collection = DbConnection.GetUserCollection();
//var firstDocument = collection.Find(new BsonDocument()).FirstOrDefault();
//Console.WriteLine(firstDocument.ToString());

#endregion

#region Read a document with Filter

//var collection = DbConnection.GetUserCollection();
//var filter = Builders<BsonDocument>.Filter.Eq("student_id", 10000);
//var firstDocument = collection.Find(filter).FirstOrDefault();
//Console.WriteLine(firstDocument.ToString());

#endregion

#region Read all document

//var collection = DbConnection.GetUserCollection();
//var documents = collection.Find(new BsonDocument()).ToList();
//Console.WriteLine(documents);

#endregion

#region Complicated Filter Example

#region Notes

// builders<bsondocument>.filter/.update gibi islemleri olusturan siniftir.
// Filter.ElemMatch<BsonValue> : kendisine verilen arrayin icindeki degerleri filtreler.
// new BsonDocument{ {} } ifadesi filtre olusturmak icin kullanilmaktadir.

//      asagidaki ornekte ElemMatch ile scores arrayine gidiliyor ve new BsonDocument ile scores icin olusturulacak filtre yazilmaya baslaniyor. type:exam filtresi yazildiktan sonra "&" anlamina gelen "," score icin filtre yazma asamasina geciliyor. score direk bir deger ile degil de bir sorgu ile deger dondurmesi beklendigi icin new BsonDocument diyerek ona >=95 anlamina gelen " "$gte",95 " sorgusu yaziliyor ve scores icin yazilmis olan filtreler sona eriyor.

// ToCursor() : ToCursor() methodu, bir MongoDB sorgusunun sonucunu bir imleç (cursor) nesnesine dönüştürür. Bir imleç nesnesi, veritabanında bir sorgu yapıldıktan sonra döndürülen belgelerin bir listesi değil, aynı zamanda bu belgelerin bir liste gibi gezilebileceği bir yapıdır. Bu sayede, veritabanında yapılan sorguların sonuçlarını parçalara bölerek, daha az bellek kullanımına ve daha yüksek performansa sahip olunur.

// Bir imleç nesnesi, sorgu sonucu olarak döndürülen belgelerin bir listesi gibi davranır. Ancak, bu nesne, veritabanına geri dönüş yaparak verileri parçalara böler ve bu parçaları sırayla alır. Bu sayede, veritabanında yapılan sorguların sonuçlarını parçalara bölerek, daha az bellek kullanımına ve daha yüksek performansa sahip olunur.

#endregion

//var collection = DbConnection.GetUserCollection();
//var examScoreFilter = Builders<BsonDocument>.Filter.ElemMatch<BsonValue>("scores",
//    new BsonDocument {
//        { "type", "exam" },
//        { "score", new BsonDocument { { "$gte", 70 } } }
//});

//var cursor = collection.Find(examScoreFilter).ToCursor();

//foreach (var document in cursor.ToEnumerable())
//{
//    global::System.Console.WriteLine(document);
//}

//await collection.Find(examScoreFilter).ForEachAsync(document => Console.WriteLine(document);
#endregion

#region Update a document

//var collection = DbConnection.GetUserCollection();
//var getUserTwo = Builders<BsonDocument>.Filter.Eq("student_id", 2);
//var updateUserTwo = Builders<BsonDocument>.Update.Set("class_id", 483);
//var result = collection.UpdateOne(getUserTwo, updateUserTwo);

#endregion

#region Update inside of array 

#region Not

// Oncelikle collection baglantimizi aliyoruz. Sonrasinda scoresTypeFilter ile Student_Id = 2 & "Type" = "quiz" olan datayi aliyoruz. Quiz bilgisini update sorgusuna tasimak icin ise "scores.$.score" diyerek degistirilecek Scores.Type bilgisini sorgumuzdan getirtiyoruz. Collection uzerinden guncelleme fonksiyonunu cagirip verileri giriyoruz.

// filter ile hangi nesne (id uzerinden) ve arrayin hangi basligi icin datayi alacagimizi belirtiyoruz.

#endregion

//var collection = DbConnection.GetUserCollection();
//var scoresTypeFilter = Builders<BsonDocument>.Filter.Eq("student_id", 2 ) & Builders<BsonDocument>.Filter.Eq("scores.type", "quiz");
//var scoresTypeScoreUpdate = Builders<BsonDocument>.Update.Set("scores.$.score", 84.92381029342834);
//collection.UpdateOne(scoresTypeFilter, scoresTypeScoreUpdate);

#endregion

#region Delete a document

//var collection = DbConnection.GetUserCollection();
//var deleteFilter = Builders<BsonDocument>.Filter.Eq("student_id", 2);
//collection.DeleteOne(deleteFilter);

#endregion


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



