using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace CategoryService.Models
{
    public class CategoryContext
    {
        private readonly IMongoDatabase database;
        MongoClient client;

        public CategoryContext(IConfiguration configuration)
        {
            client = new MongoClient(configuration.GetSection("MongoDB:ConnectionString").Value);
            database = client.GetDatabase(configuration.GetSection("MongoDB:CategoryDatabase").Value);
        }

        public IMongoCollection<Category> Categories => database.GetCollection<Category>("Categories");
    }
}
