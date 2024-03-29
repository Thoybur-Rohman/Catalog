using Catalog.Entities;
using Catalog.Repositorys;
using MongoDB.Driver;

namespace Catalog.Repositories
{
    public class MongoDbRepository : IItemRepository
    {
        private const string databaseName = "catalog";
        private const string collectionName = "items";
        private readonly IMongoCollection<Item> itemsCollection;

        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;
        public MongoDbRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            itemsCollection = database.GetCollection<Item>(collectionName);
        }
        public async Task CreateItemAsync(Item item)
        {
            await itemsCollection.InsertOneAsync(item); 
        }

        public async Task DeleteItemAsync(Guid id)
        {
          var filter = filterBuilder.Eq(item => item.Id,id);
          await itemsCollection.DeleteOneAsync(filter);
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id,id);
            return await itemsCollection.Find(filter).SingleOrDefaultAsync(); 
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
             return await itemsCollection.Find(new MongoDB.Bson.BsonDocument()).ToListAsync();
        }

        public async Task UpdateItenAsync(Item item)
        {
            var filter = filterBuilder.Eq(existingItem => existingItem.Id,item.Id);
            await itemsCollection.ReplaceOneAsync(filter,item);
        }
    }
}