using MongoDB.Bson.Serialization.Attributes;

namespace Migrator.Mongo;

[BsonIgnoreExtraElements]
public class DatabaseVideo
{
    public string Id;
    public string Title;
    public Thumbnail[] Thumbnails;
    public string UploadedAt;
    public long Views;
    [BsonIgnore] public string ViewsCount => $"{Views:N0} views";
    public DatabaseVideoAuthor Channel;
    public string Duration;

    public DatabaseVideo()
    {
    }
}