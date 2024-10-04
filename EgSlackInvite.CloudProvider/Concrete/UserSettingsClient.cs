namespace EgSlackInvite.CloudProvider.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Abstract;

    using Data;
    using Entities;
    using Microsoft.WindowsAzure.Storage.Table;

    public class UserSettingsClient : CloudStorageBase, IUserSettingsClient
    {

        private readonly HashSet<UserSettingsEntity> _whiteList = new HashSet<UserSettingsEntity>();

        /// <inheritdoc cref="IUserSettingsClient"/>
        public async Task<IEnumerable<UserSettingsEntity>> GetUserSettingsAsync(bool preserveCache = true)
        {
            if (_whiteList.Any() && preserveCache)
                return _whiteList.ToList();

            var table = this.CloudTableClient.GetTableReference("whitelist");

            // If all items are removed from the table, it ceases to exist. Go ahead and just create it if its empty.
            // This will ensure that the query just returns an empty result instead of blowing up.
            await table.CreateIfNotExistsAsync();

            var query = new TableQuery<UserSettingsEntity>();
            var users = await table.ExecuteQueryAsync(query);

            return users;
        }

        /// <inheritdoc cref="IUserSettingsClient"/>
        public async Task<bool> UserExists(string id)
        {
            if (_whiteList.Any())
                return await Task.FromResult(_whiteList.Any(x => x.PartitionKey == id));

            var table = this.CloudTableClient.GetTableReference("whitelist");

            // If all items are removed from the table, it ceases to exist. Go ahead and just create it if its empty.
            // This will ensure that the query just returns an empty result instead of blowing up.
            await table.CreateIfNotExistsAsync();

            var query = new TableQuery<UserSettingsEntity>();
            var users = await table.ExecuteQueryAsync(query);

            return await Task.FromResult(users.Any(x =>
                x.PartitionKey.Equals(id, StringComparison.InvariantCultureIgnoreCase)));
        }


        /// <inheritdoc cref="IUserSettingsClient"/>
        public async Task AddUserSettingsEntityAsync(UserSettingsEntity userSettingsEntity)
        {
            var table = this.CloudTableClient.GetTableReference("whitelist");
            await table.CreateIfNotExistsAsync();

            var userQuery = TableOperation.Retrieve(userSettingsEntity.PartitionKey, userSettingsEntity.RowKey);
            var user = await table.ExecuteAsync(userQuery);

            var operation = TableOperation.InsertOrReplace(userSettingsEntity);

            await table.ExecuteAsync(operation);
        }

        /// <inheritdoc cref="IUserSettingsClient"/>
        public async Task BatchAddOrUpdateUsersAsync(IEnumerable<UserSettingsEntity> entities)
        {
            var table = this.CloudTableClient.GetTableReference("whitelist");
            await table.CreateIfNotExistsAsync();

            // NOTE: Batch insert only works if every entity has the same partition key.
            var batchOperation = new TableBatchOperation();

            const int maxBatchSize = 100;

            if (entities.Count() > maxBatchSize)
            {
                var entitiesToInsert = entities;
                using (var enumerator = entities.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        entitiesToInsert = entitiesToInsert.Take(maxBatchSize).ToList();
                        foreach (var entity in entitiesToInsert)
                            batchOperation.InsertOrMerge(entity);

                        await table.ExecuteBatchAsync(batchOperation);
                        entitiesToInsert = entities.Skip(maxBatchSize).ToList();
                    }
                }

            }
        }

        /// <inheritdoc cref="IUserSettingsClient"/>
        public async Task RemoveUserSettingsAsync(string userId, string userEmail)
        {
            var table = this.CloudTableClient.GetTableReference("whitelist");


            var retrieveOperation = TableOperation.Retrieve<UserSettingsEntity>(userId, userEmail);
            var user = await table.ExecuteAsync(retrieveOperation);

            // The user doesn't exist.
            if (user.Result == null)
                return;

            // ETag must be set.
            // https://docs.microsoft.com/en-us/dotnet/api/microsoft.windowsazure.storage.table.tableentity.etag?view=azure-dotnet#Microsoft_WindowsAzure_Storage_Table_TableEntity_ETag
            var entity = new UserSettingsEntity
            {
                RowKey = userId,
                Email = userEmail,
                ETag = "*"
            };

            var operation = TableOperation.Delete(entity);
            await table.ExecuteAsync(operation);
        }
    }
}
