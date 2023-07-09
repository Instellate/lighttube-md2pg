using MongoDB.Bson.Serialization.Attributes;

namespace Migrator.Mongo;

[BsonIgnoreExtraElements]
public class DatabaseVideoAuthor
{
	public string Id;
	public string Name;
	public Thumbnail[] Avatars;
}