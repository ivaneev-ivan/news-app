using System.Data;
using Npgsql;
using System.Diagnostics;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using News_App.Models;

namespace News_App.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _configuration;

    public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    private NpgsqlConnection Connection => new(_configuration.GetConnectionString("default"));

    public IActionResult Index()
    {
        var items = getNews();
        return View(items);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public List<News> getNews()
    {
        using (IDbConnection db = Connection)
        {
            List<News> result = db.Query<News>("SELECT * FROM news").ToList();
            return result;
        }
    }

    public class News
    {
        public int id { set; get; }
        public string title { set; get; }
        public string description { set; get; }
        public DateTime created_at { set; get; }
    }
}