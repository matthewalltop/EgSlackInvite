namespace EgSlackInvite.CloudProvider.Abstract
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;

    public interface IUserSettingsClient
    {

        /// <summary>
        /// Returns the user white list from cloud storage.
        /// </summary>
        /// <returns>A <see cref="Task{T}"/></returns>
        Task<IEnumerable<UserSettingsEntity>> GetUserSettingsAsync(bool preserveCache = true);


        /// <summary>
        /// Indicates if a user for the given id exists in the whitelist.
        /// </summary>
        /// <param name="id">The user Id to search for</param>
        /// <returns>A <see cref="Task{T}"/></returns>
        Task<bool> UserExists(string id);

        /// <summary>
        /// Insert a user into the user whitelist
        /// </summary>
        /// <param name="userSettingsEntity">The entity to create.</param>
        /// <returns>A <see cref="Task"/></returns>
        Task AddUserSettingsEntityAsync(UserSettingsEntity userSettingsEntity);

        /// <summary>
        /// Inserts a batch of entities into the user whitelist.
        /// </summary>
        /// <param name="entities">The entities to insert/></param>
        /// <returns>A <see cref="Task"/></returns>
        Task BatchAddOrUpdateUsersAsync(IEnumerable<UserSettingsEntity> entities);

        /// <summary>
        /// Removes a user from the user whitelist
        /// </summary>
        /// <param name="userId">The Id of the user/></param>
        /// <param name="userEmail">The email of the User</param>
        /// <returns>A <see cref="Task"/></returns>
        Task RemoveUserSettingsAsync(string userId, string userEmail);
    }
}
