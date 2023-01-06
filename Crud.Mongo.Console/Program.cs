using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.Runtime.CompilerServices;

Console.WriteLine("Yildiray Kocak");
Console.WriteLine("MongoDB CRUD Operations");


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
//foreach (var item in documents)
//{
//    global::System.Console.WriteLine(item.ToString());
//}

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
//var scoresTypeFilter = Builders<BsonDocument>.Filter.Eq("student_id", 2) & Builders<BsonDocument>.Filter.Eq("scores.type", "quiz");
//var scoresTypeScoreUpdate = Builders<BsonDocument>.Update.Set("scores.$.score", 84.92381029342834);
//collection.UpdateOne(scoresTypeFilter, scoresTypeScoreUpdate);

#endregion

#region Delete a document

//var collection = DbConnection.GetUserCollection();
//var deleteFilter = Builders<BsonDocument>.Filter.Eq("student_id", 2);
//collection.DeleteOne(deleteFilter);

#endregion

#region Create a new document based on a Class

//List<BScore> scores2 = new()
//{
//    new BScore{ Type = "exam",Score = 100 },
//    new BScore{ Type = "quiz", Score = 23 },
//    new BScore{ Type = "homework", Score = 34 },
//    new BScore{ Type = "efe", Score = 53 }
//};

//Student student = new()
//{
//    ClassId = 500,
//    StudentId = 2,
//    Scores = scores2
//};

//var collection = DbConnection.GetUserCollection();
//collection.InsertOne(student.ToBsonDocument());

#endregion

#region Read a document with filter on Class

//var collection = DbConnection.GetUserCollectionStudent();
//var filter = Builders<Student>.Filter.Eq(x => x.ClassId, 500);
//var result = collection.Find(filter).FirstOrDefault();

//foreach (var item in result.Scores)
//{
//    global::System.Console.WriteLine("Type : " + item.Type + " Score : " + item.Score);
//}

#endregion

#region Complicated Filter Example With Class

//var collection = DbConnection.GetUserCollectionStudent();
//var examScoreFilter = Builders<Student>.Filter.ElemMatch<BScore>(x => x.Scores, x => x.Type == "exam" && x.Score >= 70); // Ana obje, donulecek obje tipi /// Elem Match icin array icindeki objenin tipi verilmeli.
//var result = collection.Find(examScoreFilter).ToCursor();

//foreach (var item in result.ToEnumerable())
//{
//    global::System.Console.WriteLine(item.StudentId);
//}

#endregion

#region Update a document based on a Class

//var collection = DbConnection.GetUserCollectionStudent();
//var filter = Builders<Student>.Filter.Eq(x => x.StudentId, 2);
//var update = Builders<Student>.Update.Set(x => x.ClassId, 485);
//var student_value = collection.Find(filter).FirstOrDefault();
//Console.WriteLine(student_value.ClassId);
//collection.UpdateOne(filter, update);
//var student_updated_value = collection.Find(filter).FirstOrDefault();
//Console.WriteLine(student_updated_value.ClassId);

#endregion

#region Update inside of array based on a Class

//var collection = DbConnection.GetUserCollectionStudent();
//var studentIdFilter = Builders<Student>.Filter.Eq(x => x.StudentId, 2);
//var examScoreFilter = Builders<Student>.Filter.ElemMatch(x => x.Scores, x=> x.Type == "exam");
//var combinedFilter = Builders<Student>.Filter.And(studentIdFilter, examScoreFilter);
//var update = Builders<Student>.Update.Set("Scores.$.Score", 50);
//collection.UpdateOne(combinedFilter, update);

#endregion

#region Delete a document based on a Class

//var collection = DbConnection.GetUserCollectionStudent();
//var filter = Builders<Student>.Filter.Eq(x => x.StudentId, 2);
//collection.DeleteOne(filter);

#endregion

public static class DbConnection
{
    public static IMongoCollection<Student> GetUserCollectionStudent()
    {
        var settings = MongoClientSettings.FromConnectionString("mongodb://localhost:27017/");
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        var client = new MongoClient(settings);
        var database = client.GetDatabase("FirstTry");
        var collection = database.GetCollection<Student>("User");
        return collection;
    }
    
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

public class Student
{
    [BsonId]
    public ObjectId Id { get; set; }

    [BsonElement(nameof(StudentId))]
    public int StudentId { get; set; }

    [BsonElement(nameof(Scores))]
    public List<BScore> Scores { get; set; }

    [BsonElement(nameof(ClassId))]
    public int ClassId { get; set; }
}

public class BScore
{
    [BsonElement(nameof(Type))]
    public string Type { get; set; }

    [BsonElement(nameof(Score))]
    public double Score { get; set; }
}
