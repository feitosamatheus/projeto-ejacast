using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProjetoEjaCast.Models;

namespace ProjetoEjaCast.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IWebHostEnvironment _environment;

    public HomeController(ILogger<HomeController> logger, IWebHostEnvironment environment)
    {
        _logger = logger;
        _environment = environment;
    }

    public IActionResult Index()
    {
        var caminhoArquivo = Path.Combine(
            _environment.WebRootPath,
            "dados",
            "contador.txt"
        );

        int contador = 0;

        bool jaVisitou = Request.Cookies.ContainsKey("visitou_ejacast");

        if (!jaVisitou)
        {
            if (System.IO.File.Exists(caminhoArquivo))
            {
                int.TryParse(
                    System.IO.File.ReadAllText(caminhoArquivo),
                    out contador
                );
            }

            contador++;

            System.IO.File.WriteAllText(
                caminhoArquivo,
                contador.ToString()
            );

            Response.Cookies.Append(
                "visitou_ejacast",
                "true",
                new CookieOptions
                {
                    Expires = DateTimeOffset.Now.AddYears(1),
                    HttpOnly = true,
                    IsEssential = true
                });
        }

        if (System.IO.File.Exists(caminhoArquivo))
        {
            int.TryParse(
                System.IO.File.ReadAllText(caminhoArquivo),
                out contador
            );
        }

        ViewBag.Contador = contador;

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
