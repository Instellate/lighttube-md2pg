using Migrator.Mongo;

namespace Migrator.Database.Models;

public class Playlist
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required PlaylistVisibility Visibility { get; set; }
    public required List<PlaylistVideoID> Videos { get; set; }
    public required User Author { get; set; }
    public required DateTimeOffset LastUpdated { get; set; }
}