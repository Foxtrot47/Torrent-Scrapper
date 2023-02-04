using Microsoft.AspNetCore.Mvc.RazorPages;
using TorrentScaperUtil;

namespace Torrent_Scrapper_Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public List<ScrapeData> TorrentData { get; set; }
        public int PageCount  {get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnPost()
        {
            if (Request.Form["query"] == "")
            return;
            var keywords = Request.Form["query"].ToString().Split(" ");
            var scrap = new Scraper();
            var result = scrap.GetResults(keywords);
            TorrentData = result.Data;
            PageCount = result.PageCount;
        }

        public void OnGet()
        {
        }
    }
}