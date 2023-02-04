namespace TorrentScaperUtil
{
    public class ScrapeData
    {
        public string WebSiteName { get; set; }
        public string TorrentName { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
        public string Language { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string TotalSize { get; set; }
        public string Downloads { get; set; }
        public string Uploader { get; set; }
        public string DateUploaded { get; set; }
        public int SeedersCount { get; set; }
        public int LeechersCount { get; set; }
        public string MagnetLink { get; set; }
        public string TorrentLink { get; set; }
        public string ImageUrl { get; set; }
        public string OriginUrl { get; set; }
    }
}
