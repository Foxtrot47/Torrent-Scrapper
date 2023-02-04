namespace Torrent_Scaper
{
    public class ScrapeData
    {
        public ScrapeData(string webSiteName, string torrentName, string category, string language, string description, string title, string totalSize, string downloads, string dateUploaded, int seedersCount, int leechersCount, string magnetLink, string torrentLink, string imageUrl, string originUrl, int totalPages, int currentPage)
        {
            WebSiteName=webSiteName;
            TorrentName=torrentName;
            Category=category;
            Language=language;
            Description=description;
            Title=title;
            TotalSize=totalSize;
            Downloads=downloads;
            DateUploaded=dateUploaded;
            SeedersCount=seedersCount;
            LeechersCount=leechersCount;
            MagnetLink=magnetLink;
            TorrentLink=torrentLink;
            ImageUrl=imageUrl;
            OriginUrl=originUrl;
            TotalPages=totalPages;
            CurrentPage=currentPage;
        }

        public string WebSiteName { get; set; }
        public string TorrentName { get; set; }
        public string Category { get; set; }
        public string Language { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string TotalSize { get; set; }
        public string Downloads { get; set; }
        public string DateUploaded { get; set; }
        public int SeedersCount { get; set; }
        public int LeechersCount { get; set; }
        public string MagnetLink { get; set; }
        public string TorrentLink { get; set; }
        public string ImageUrl { get; set; }
        public string OriginUrl { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }

    }
}
