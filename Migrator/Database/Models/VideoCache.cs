namespace Migrator.Database.Models;

public class VideoCache
{
    public required string Id { get; set; }
    public required string Title { get; set; }
    public required string Thumbnail { get; set; }
    public required long Views { get; set; }
    public required Channel Channel { get; set; }
    public required string Duration { get; set; }
}