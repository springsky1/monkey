using System;
using System.Collections.Generic;
using System.Text;

using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System.Linq.Expressions;

namespace MyProject.MyClass
{
    public class MongodbHelper <T>
    {
        static MongoClient client = null;
        public static void Test()
        {
            var database = client.GetDatabase("admin");
            var collection = database.GetCollection<BsonDocument>("mycollection");
            BsonDocument bsons = new BsonDocument {
                { "username","test1"} ,{ "type", "大类" },
                { "number", 5 },{ "info",new BsonDocument{ { "info1", "info1_value" } } }
            };
          //  AddDocument(collection, bsons);
            // List<BsonDocument> bs = new List<BsonDocument>();
            // for (int i=2;i< 20; i++)
            // {
            //     BsonDocument bsonsNew = new BsonDocument {
            //     { "username","test"+i} ,{ "type", "大类" },
            //     { "number", i },{ "info",new BsonDocument{ { "info"+i, "info"+i+"_value" } } }
            // };
            //     bs.Add(bsonsNew);
            // }
            // AddManyDocument(collection, bs);
            // query = "{username:/test/}";
            //FindDocumnets(collection,"");

            //  UpdateDocument(collection, "{username:'test1'}", "{username:'test98989'}");
            var query = "{username:'test1'}";
          //  Delete(collection, query);
           // Deletemany(collection, query);
        }

        #region 基础方法
        static MongodbHelper()
        {
            client = new MongoClient("mongodb://127.0.0.1:27017");
        }
        public MongodbHelper(string url)
        {
            client = new MongoClient(url);
        }

        public static IMongoDatabase GetDatabase(string databseName)
        {
            return client.GetDatabase(databseName);

        }
        public static IMongoCollection<BsonDocument> GetCollection(IMongoDatabase database, string collectionName)
        {

            return database.GetCollection<BsonDocument>("mycollection");
        }
        #endregion
        
        /// <summary>
        /// 添加一个文档到指定的Collection 里面
        /// </summary>
        /// <param name="mongoCollection"></param>
        /// <param name="bsons"></param>
        public static void AddDocument(IMongoCollection<BsonDocument>  mongoCollection, BsonDocument bsons) {
            mongoCollection.InsertOne(bsons);
        }
        /// <summary>
        /// 添加多个文档到Collection 里面
        /// </summary>
        /// <param name="mongoCollection"></param>
        /// <param name="bsons"></param>
        public static void AddManyDocument(IMongoCollection<BsonDocument> mongoCollection, List<BsonDocument> bsons)
        {
            mongoCollection.InsertMany(bsons);
        }
        /// <summary>
        /// 查询数据  参考 query = "{username:/test/}";
        /// </summary>
        /// <param name="mongoCollection"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static IFindFluent<BsonDocument, BsonDocument> FindDocumnets(IMongoCollection<BsonDocument> mongoCollection,string query) {
            // query = "{username:/test/}";
            var filter = BsonSerializer.Deserialize<BsonDocument>(query);

            IFindFluent<BsonDocument, BsonDocument> docs = mongoCollection.Find(filter);
           // List<BsonDocument> docs = mongoCollection.Find(filter).ToList<BsonDocument>();
            return docs;
        }

        public static UpdateResult UpdateDocument(IMongoCollection<BsonDocument> mongoCollection, string query,string doc) {
            var filter = BsonSerializer.Deserialize<BsonDocument>(query);


            UpdateDefinition<BsonDocument> updateDefinition = BsonSerializer.Deserialize<BsonDocument>(doc);
            UpdateResult updateResult = mongoCollection.UpdateOne(filter, updateDefinition);
            return updateResult;

        }

        public void testsss() {


            Func<BsonDocument, bool> func1 =x=>true;


        }

        public static DeleteResult Delete(IMongoCollection<BsonDocument> mongoCollection, string query) {

   
            var filter = Builders<BsonDocument>.Filter.Eq("username","test1");   
            
            return mongoCollection.DeleteOne( filter);
        }

        // public static DeleteResult Deletemany(IMongoCollection<BsonDocument> mongoCollection, string query){

        //      var filter = BsonSerializer.Deserialize<BsonDocument>(query);
        //       return  mongoCollection.DeleteMany(query);
        // }
    }
}
