using Api.Apps.AdminApi.Dtos;
using Api.Services;
using Core.Entities;
using Data;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Newtonsoft.Json.Linq;

namespace Api.Apps.AdminApi.Controllers
{
    [Route("admin/api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly JwtService _jwtService;
        private readonly ShopDbContext _context;
        private readonly IConverter _converter;
        private readonly IWebHostEnvironment _env;

        public AuthController(UserManager<AppUser> userManager, JwtService jwtService,ShopDbContext context,IConverter converter,IWebHostEnvironment env)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _context = context;
            _converter = converter;
            _env = env;
        }

        [HttpGet("pdfdownload")]
        public IActionResult GetReport()
        {
            string body = string.Empty;

            using (StreamReader reader = new StreamReader("wwwroot/html/report.html"))
            {
                body = reader.ReadToEnd();
            }

            string tableBody = "";

            foreach (var item in _context.Products.ToList())
            {
                string tr = $@"
                                <tr>
                                    <td>{item.Id}</td>
                                    <td>{item.Name}</td>
                                    <td>{item.SalePrice}</td>
                                </tr>";
                tableBody += tr;
            }

            body = body.Replace("{{body}}", tableBody);

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",
                Out = Path.Combine(_env.WebRootPath, "pdf", "report.pdf")
            };
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = body,
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(_env.WebRootPath, "html", "bootstrap.min.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line =     true, Center = "Report Footer" }
            };
            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };
            var file = _converter.Convert(pdf);

            var filePath = Path.Combine(_env.WebRootPath, "pdf", "report.pdf");
            var type = "application/pdf";

            var bytes = System.IO.File.ReadAllBytesAsync(filePath).Result;

            System.IO.File.Delete(filePath);

            return File(bytes, type, Path.GetFileName(filePath));
        }
      

        [HttpGet("createadmin")]
        public async Task<IActionResult> CreateAdmin()
        {
            AppUser appUser = new AppUser
            {
                UserName = "admin",
                FullName = "Hikmet Abbasov",
                Email = "hiko@code.edu.az",
                IsAdmin = true,
            };

            await _userManager.CreateAsync(appUser, "Admin@123");
            await _userManager.AddToRoleAsync(appUser, "SuperAdmin");

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AdminLoginDto dto)
        {
            AppUser user = await _userManager.FindByNameAsync(dto.UserName);

            if (user == null || !user.IsAdmin)
                return NotFound();

            if (!await _userManager.CheckPasswordAsync(user, dto.Password))
                return BadRequest();

            var token = _jwtService.Generate(user, await _userManager.GetRolesAsync(user));

            return Ok(new { token = token });
        }
    }
}
