using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Migrator.Mongo;

[BsonIgnoreExtraElements]
public class DatabaseUser
{
	private const string INNERTUBE_GRID_RENDERER_TEMPLATE = "{\"items\": [%%CONTENTS%%]}";

	private const string INNERTUBE_MESSAGE_RENDERER_TEMPLATE =
		"{\"messageRenderer\":{\"text\":{\"simpleText\":\"%%MESSAGE%%\"}}}";

	private const string ID_ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_";
	public string UserID { get; set; }
	[JsonIgnore] public string PasswordHash { get; set; }
	[JsonIgnore] public Dictionary<string, SubscriptionType> Subscriptions { get; set; }
	public string LTChannelID { get; set; }

	[JsonIgnore] [BsonIgnoreIfNull] [Obsolete("Use Subscriptions dictionary instead")]
	public string[]? SubscribedChannels;

	[JsonIgnore] [BsonIgnoreIfNull] [Obsolete("Use UserID instead")]
	public string? Email;

}