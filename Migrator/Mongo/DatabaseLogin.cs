using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Migrator.Mongo;

[BsonIgnoreExtraElements]
public class DatabaseLogin
{
	public string Id;
	public string UserID;
	public string Token;
	[JsonIgnore] public string UserAgent;
	public string[] Scopes;
	[JsonIgnore] public DateTimeOffset Created = DateTimeOffset.MinValue;
	[JsonIgnore] public DateTimeOffset LastSeen = DateTimeOffset.MinValue;
}