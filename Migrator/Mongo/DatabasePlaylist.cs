namespace Migrator.Mongo;

public class DatabasePlaylist
{
    private const string INNERTUBE_PLAYLIST_INFO_TEMPLATE = "{\"playlistId\":\"%%PLAYLIST_ID%%\",\"title\":\"%%TITLE%%\",\"totalVideos\":%%VIDEO_COUNT%%,\"currentIndex\":%%CURRENT_INDEX%%,\"localCurrentIndex\":%%CURRENT_INDEX%%,\"longBylineText\":{\"runs\":[{\"text\":\"%%CHANNEL_TITLE%%\",\"navigationEndpoint\":{\"browseEndpoint\":{\"browseId\":\"%%CHANNEL_ID%%\"}}}]},\"isInfinite\":false,\"isCourse\":false,\"ownerBadges\":[],\"contents\":[%%CONTENTS%%]}";
    private const string INNERTUBE_GRID_PLAYLIST_RENDERER_TEMPLATE = "{\"gridPlaylistRenderer\":{\"playlistId\":\"%%ID%%\",\"title\":{\"simpleText\":\"%%TITLE%%\"},\"videoCountShortText\":{\"simpleText\":\"%%VIDEOCOUNT%%\"},\"thumbnailRenderer\":{\"playlistVideoThumbnailRenderer\":{\"thumbnail\":{\"thumbnails\":[{\"url\":\"%%THUMBNAIL%%\",\"width\":0,\"height\":0}]}}}}}";
    private const string ID_ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_";
    public string Id;
    public string Name;
    public string Description;
    public PlaylistVisibility Visibility;
    public List<string> VideoIds;
    public string Author;
    public DateTimeOffset LastUpdated;
}

public enum PlaylistVisibility
{
    PRIVATE,
    UNLISTED,
    VISIBLE
}