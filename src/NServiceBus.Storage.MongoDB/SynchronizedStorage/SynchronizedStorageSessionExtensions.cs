﻿using System;
using MongoDB.Driver;
using NServiceBus.Persistence;
using NServiceBus.Storage.MongoDB;

namespace NServiceBus
{
    /// <summary>
    ///
    /// </summary>
    public static class SynchronizedStorageSessionExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static IClientSessionHandle GetClientSession(this SynchronizedStorageSession session)
        {
            Guard.AgainstNull(nameof(session), session);

            if (session is StorageSession storageSession)
            {
                return storageSession.MongoSession;
            }

            throw new Exception($"Cannot access the synchronized storage session. Ensure that 'EndpointConfiguration.UsePersistence<{nameof(MongoPersistence)}>()' has been called.");
        }
    }
}