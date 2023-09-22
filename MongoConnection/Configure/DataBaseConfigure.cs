using MongoDB.Driver;

namespace MongoConnection.Configure
{
    public class DataBaseConfigure
    {
        public static MongoClient ConfigureConnection()
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            return client;
        }
    }
}
