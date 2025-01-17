﻿namespace NServiceBus
{
    using System;
    using MongoDB.Driver;
    using Persistence;
    using Storage.MongoDB;

    /// <summary>
    /// MongoDB persistence specific extension methods for the <see cref="ISynchronizedStorageSession"/>.
    /// </summary>
    public static class SynchronizedStorageSessionExtensions
    {
        /// <summary>
        /// Retrieves the current MongoDB client session from the context.
        /// </summary>
        public static IClientSessionHandle GetClientSession(this ISynchronizedStorageSession session)
        {
            Guard.AgainstNull(nameof(session), session);

            if (session is IMongoSessionProvider storageSession)
            {
                return storageSession.MongoSession;
            }

            throw new Exception($"Cannot access the synchronized storage session. Ensure that 'EndpointConfiguration.UsePersistence<{nameof(MongoPersistence)}>()' has been called.");
        }
    }
}