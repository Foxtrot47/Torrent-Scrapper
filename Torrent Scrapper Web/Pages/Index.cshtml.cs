using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TorrentScaperUtil;

namespace Torrent_Scrapper_Web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public List<ScrapeData> TorrentData { get; set; }
    public int PageCount { get; set; }

    [BindProperty(Name="pagenum", SupportsGet = true)] public int CurrentPage { get; set; } = 1;
    [BindProperty(Name="query", SupportsGet = true)] public string SearchKeywords { get; set; }

    public void OnGet()
    {
        if (string.IsNullOrEmpty(SearchKeywords))
            return;
        var keywords = SearchKeywords.Split(" ");
        var scrap = new Scraper();
        var result = scrap.GetResults(keywords, CurrentPage);
        TorrentData = result.Data;
        PageCount = result.PageCount;
    }
}