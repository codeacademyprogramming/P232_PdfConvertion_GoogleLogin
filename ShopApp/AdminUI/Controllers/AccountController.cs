using AdminUI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AdminUI.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginViewModel admin)
        {
            AdminLoginResponseViewModel data = new AdminLoginResponseViewModel();
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(admin), Encoding.UTF8, "application/json");
                using(var response = await client.PostAsync("https://localhost:7137/admin/api/auth/login", content))
                {
                    if(response.IsSuccessStatusCode)
                    {
                        data = JsonConvert.DeserializeObject<AdminLoginResponseViewModel>(await response.Content.ReadAsStringAsync());
                        Response.Cookies.Append("token", data.Token);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Username or Password incorrect");
                        return View();
                    }
                }
            }

            return RedirectToAction("index", "home");
        }
    }
}
