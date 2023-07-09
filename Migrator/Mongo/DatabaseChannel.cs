using MongoDB.Bson.Serialization.Attributes;

namespace Migrator.Mongo;

[BsonIgnoreExtraElements]
public class DatabaseChannel
{
	public string ChannelId;
	public string Name;
	public string Subscribers;
	public string IconUrl;

	public DatabaseChannel() {
		
	}
}