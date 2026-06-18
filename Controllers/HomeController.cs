using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using projeto_ejacast.config;
using projeto_ejacast.Models;
using ProjetoEjaCast.Models;

namespace ProjetoEjaCast.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;

    }

    public IActionResult Index()
    {
        var contador = _context.ContadorVisitas.FirstOrDefault();

        if (contador == null)
        {
            contador = new ContadorVisita
            {
                Total = 0
            };

            _context.ContadorVisitas.Add(contador);
            _context.SaveChanges();
        }

        bool jaVisitou = Request.Cookies.ContainsKey("visitou_ejacast");

        if (!jaVisitou)
        {
            contador.Total++;

            _context.ContadorVisitas.Update(contador);
            _context.SaveChanges();

            Response.Cookies.Append(
                "visitou_ejacast",
                "true",
                new CookieOptions
                {
                    Expires = DateTimeOffset.Now.AddYears(1),
                    HttpOnly = true,
                    IsEssential = true
                }
            );
        }

        ViewBag.Contador = contador.Total;

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult SobreNos()
    {
        return View();
    }

    public IActionResult Episodios()
    {
        return View();
    }

    public IActionResult Desenvolvedores()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
