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
        await MigrateLogins(db, tokensColl);
        await MigrateVideoCache(db, videoCacheColl);
        await MigratePlaylistCache(db, playlistColl);
        await MigrateChannels(db, channelCacheColl);
        await MigrateOAuthTokens(db, oauth2TokensColl);
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
                Id = user.UserID,
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

    public static async Task MigrateVideoCache(PrimaryDb db, IMongoCollection<DatabaseVideo> videoColl)
    {
        IReadOnlyList<DatabaseVideo> videos = await (await videoColl.FindAsync(_ => true)).ToListAsync();
        foreach (DatabaseVideo video in videos)
        {
            VideoCache dbVideo = new()
            {
                Id = video.Id,
                Title = video.Title,
                Thumbnail = video.Thumbnails[0].Url.ToString(),
                Views = video.Views,
                Channel = new()
                {
                    Id = video.Channel.Id,
                    Name = video.Channel.Name,
                    Avatar = video.Channel.Avatars[0].Url.ToString(),
                },
                Duration = video.Duration
            };
            db.VideoCache.Add(dbVideo);
        }
        await db.SaveChangesAsync();
    }

    public static async Task MigratePlaylistCache(PrimaryDb db, IMongoCollection<DatabasePlaylist> playlistColl)
    {
        IReadOnlyList<DatabasePlaylist> playlists = await (await playlistColl.FindAsync(_ => true)).ToListAsync();
        foreach (DatabasePlaylist playlist in playlists)
        {
            Playlist dbPlaylist = new()
            {
                Id = playlist.Id,
                Name = playlist.Name,
                Description = playlist.Description,
                Visibility = playlist.Visibility,
                Videos = new(),
                Author = (await db.Users.FindAsync(playlist.Author))!,
                LastUpdated = playlist.LastUpdated
            };

            List<PlaylistVideoID> videoIDs = new(playlist.VideoIds.Count);
            foreach (string id in playlist.VideoIds)
            {
                videoIDs.Add(new()
                {
                    Playlist = dbPlaylist,
                    Video = (await db.VideoCache.FindAsync(id))!
                });
            }
            db.Playlists.Add(dbPlaylist);
        }
        await db.SaveChangesAsync();
    }

    public static async Task MigrateChannels(PrimaryDb db, IMongoCollection<DatabaseChannel> channelColl)
    {
        IReadOnlyList<DatabaseChannel> channels = await (await channelColl.FindAsync(_ => true)).ToListAsync();
        foreach (DatabaseChannel channel in channels)
        {
            Channel dbChannel = new()
            {
                Id = channel.ChannelId,
                Name = channel.Name,
                Avatar = channel.IconUrl.ToString(),
            };
            db.Channels.Add(dbChannel);
        }
        await db.SaveChangesAsync();
    }

    public static async Task MigrateOAuthTokens(PrimaryDb db, IMongoCollection<DatabaseOauthToken> tokensColl)
    {
        IReadOnlyList<DatabaseOauthToken> tokens = await (await tokensColl.FindAsync(_ => true)).ToListAsync();
        foreach (DatabaseOauthToken token in tokens)
        {
            OAuthToken dbToken = new()
            {
                UserId = token.UserId,
                ClientId = token.ClientId,
                RefreshToken = token.RefreshToken,
                CurrentAuthToken = token.CurrentAuthToken,
                Scopes = string.Join(' ', token.Scopes),
                CurrentTokenExpirationDate = token.CurrentTokenExpirationDate
            };
            db.OAuthTokens.Add(dbToken);
        }
        await db.SaveChangesAsync();
    }
}