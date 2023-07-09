using MongoDB.Bson.Serialization.Attributes;

namespace Migrator.Mongo;

[BsonIgnoreExtraElements]
public class DatabaseOauthToken
{
	public string UserId;
	public string? ClientId;
	public string RefreshToken;
	public string CurrentAuthToken;
	public string[] Scopes;
	public DateTimeOffset CurrentTokenExpirationDate;
}