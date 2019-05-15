﻿namespace NServiceBus.Persistence.MongoDb.Tests.SubscriptionPersistence
{
    using System;
    using System.Globalization;
    using global::MongoDB.Bson;
    using global::MongoDB.Driver;
    using NServiceBus.Persistence.MongoDB.Database;
    using NServiceBus.Persistence.MongoDB.Subscriptions;
    using NServiceBus.Unicast.Subscriptions.MessageDrivenSubscriptions;
    using NUnit.Framework;

    public class MongoFixture
    {
        private SubscriptionPersister _storage;
        private IMongoDatabase _database;
        private MongoClient _client;
        private readonly string _databaseName = "Test_" + DateTime.Now.Ticks.ToString(CultureInfo.InvariantCulture);

        [SetUp]
        public void SetupContext()
        {
            var connectionString = ConnectionStringProvider.GetConnectionString();

            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(_databaseName);
            _storage = new SubscriptionPersister(_database);

            ((IInitializableSubscriptionStorage)_storage).Init();
        }

        protected SubscriptionPersister Storage => _storage;

        protected IMongoCollection<Subscription> Subscriptions => _database.GetCollection<Subscription>(MongoPersistenceConstants.SubscriptionCollectionName);

        [TearDown]
        public void TeardownContext() => _client.DropDatabase(_databaseName);
    }

    public static class Extentions
    {
        public static long Count<TDocument>(this IMongoCollection<TDocument> collection)
        {
            return collection.Count(new BsonDocument());
        }
    }
}