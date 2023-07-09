using Migrator.Database;
using Migrator.Database.Models;
using Migrator.Mongo;
using MongoDB.Driver;

namespace Migrator;

public static class Migrate
{
    public static async Task StartMigration(PrimaryDb db, IMongoDatabase mongo)
    {
        // Coll = collection
        var usersColl = mongo.GetCollection<DatabaseUser>("users");
        var tokensColl = mongo.GetCollection<DatabaseLogin>("tokens");
        var videoCacheColl = mongo.GetCollection<DatabaseVideo>("videoCache");
        var playlistColl = mongo.GetCollection<DatabasePlaylist>("playlists");
        var channelCacheColl = mongo.GetCollection<DatabaseChannel>("channelCache");
        var oauth2TokensColl = mongo.GetCollection<DatabaseOauthToken>("oauth2Tokens");

        await MigrateUsers(db, usersColl);
    }

    public static async Task MigrateUsers(PrimaryDb db, IMongoCollection<DatabaseUser> usersColl)
    {
        IReadOnlyList<DatabaseUser> users = await (await usersColl.FindAsync(_ => true)).ToListAsync();
        foreach (DatabaseUser user in users)
        {
            List<UserSubscription> subscriptions = new(user.Subscriptions.Count);
            foreach ((string key, SubscriptionType value) in user.Subscriptions)
            {
                subscriptions.Add(new()
                {
                    Id = key,
                    Type = value
                });
            }

            User dbUser = new()
            {
                UserId = user.UserID,
                PasswordHash = user.PasswordHash,
                Subscriptions = subscriptions,
                LTChannelID = user.LTChannelID
            };
            db.Users.Add(dbUser);
        }

        await db.SaveChangesAsync();
    }

    public static async Task MigrateLogins(PrimaryDb db, IMongoCollection<DatabaseLogin> tokensColl)
    {
        IReadOnlyList<DatabaseLogin> logins = await (await tokensColl.FindAsync(_ => true)).ToListAsync();
        foreach (DatabaseLogin login in logins)
        {
            Login dbLogin = new()
            {
                Id = login.Id,
                User = (await db.Users.FindAsync(login.UserID))!,
                Token = login.Token,
                UserAgent = login.UserAgent,
                Created = login.Created,
                LastSeen = login.LastSeen
            };
            db.Logins.Add(dbLogin);
        }
        await db.SaveChangesAsync();
    }

}