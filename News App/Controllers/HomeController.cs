using System.Data;
using System.Diagnostics;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using News_App.Models;
using Npgsql;

namespace News_App.Controllers;

public class HomeController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<HomeController> _logger;

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

    public List<CreateNewsViewModel> getNews()
    {
        using (IDbConnection db = Connection)
        {
            var result = db.Query<CreateNewsViewModel>("SELECT * FROM news").ToList();
            return result;
        }
    }

    [HttpGet]
    public IActionResult CreateNews()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateNews(CreateNewsViewModel
        model)
    {
        if (ModelState.IsValid)
        {
            using (IDbConnection db = Connection)
            {
                db.Query($"INSERT INTO news (title, description) VALUES ('{model.title}', '{model.description}')");
            }

            return Redirect("/Home/Index");
        }

        return View();
    }
}