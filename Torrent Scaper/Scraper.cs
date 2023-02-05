using AngleSharp;

namespace TorrentScaperUtil
{
    public class Scraper
    {
        public List<ScrapeData> Data;
        public int PageCount;
        public int CurrentPage;

        public Scraper()
        {
            Data = new List<ScrapeData>();
            PageCount=0;
        }

        public Scraper GetResults(string[] arr,int pageNumber)
        {
            Data = Data.Concat(Scrape1337XPages(arr,pageNumber).Result).ToList();
            return this;
        }
        public async Task<List<ScrapeData>> Scrape1337XPages (string[] keywords, int pageNumber = 1)
        {
            const string domain = "https://1337x.to";
            var searchKeywords = keywords.Aggregate(
                "",
                (current, next) => current + "+" + next);

            var config = Configuration.Default.WithDefaultLoader();
            var url = domain + "/search/" + searchKeywords + "/" + pageNumber + "/";
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(url);
            const string tableSelector = "div.table-list-wrap > table > tbody > tr > td.coll-1.name > a:nth-child(2)";
            var tableRows = document.QuerySelectorAll(tableSelector);
            var links = tableRows.Select(row => row.GetAttribute("href")).ToList();
            PageCount = document.QuerySelectorAll("div.pagination > ul > li").Length;
            var data = new List<ScrapeData>(links.Count);

            Parallel.ForEach(links, row => data.Add(Scrape1337xData(row!)?.Result));

            return data;
        }

        private async Task<ScrapeData> Scrape1337xData (string link)
        {
            const string domain = "https://1337x.to";
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(domain + link);
            if (string.IsNullOrEmpty(document.QuerySelector("div.box-info-heading.clearfix")?.TextContent))
                return null;
            return new ScrapeData()
            {
                Title = document.QuerySelector("div.box-info-heading.clearfix")?.TextContent.Trim(),
                TorrentName = document.QuerySelector("div.box-info-heading.clearfix")?.TextContent.Trim(),
                Category = document.QuerySelector("div.clearfix > ul:nth-child(2) > li:nth-child(1) > span")?.TextContent.Trim(),
                Type = document.QuerySelector("div.clearfix > ul:nth-child(2) > li:nth-child(2) > span")?.TextContent.Trim(),
                Language = document.QuerySelector("div.clearfix > ul:nth-child(2) > li:nth-child(3) > span")?.TextContent.Trim(),
                TotalSize = document.QuerySelector("div.clearfix > ul:nth-child(2) > li:nth-child(4) > span")?.TextContent.Trim(),
                Downloads = document.QuerySelector("div.clearfix > ul:nth-child(3) > li:nth-child(1) > span")?.TextContent.Trim(),
                DateUploaded = document.QuerySelector("div.clearfix > ul:nth-child(2) > li:nth-child(3) > span")?.TextContent.Trim(),
                SeedersCount = int.TryParse(document.QuerySelector("span.seeds")?.TextContent.Trim(), out int seeders) ? seeders : default(int),
                LeechersCount = int.TryParse(document.QuerySelector("span.leeches")?.TextContent.Trim(), out int leechers) ? leechers : default(int),
                MagnetLink = document.QuerySelector("div.clearfix > ul> li:nth-child(1) > a")?.GetAttribute("href"),
                TorrentLink = document.QuerySelector("li.dropdown > ul > li:nth-child(1) > a")?.GetAttribute("href"),
                Description = document.QuerySelector("#mCSB_1_container > p")?.TextContent.Trim(),
                WebSiteName = "1337x",
                ImageUrl = document.QuerySelector("div.torrent-image-wrap > div > img")?.GetAttribute("src"),
                OriginUrl = domain + link,
                Uploader = document.QuerySelector("div.clearfix > ul:nth-child(2) > li:nth-child(5) > span > a")?.TextContent.Trim()
            };
        }
    }
}