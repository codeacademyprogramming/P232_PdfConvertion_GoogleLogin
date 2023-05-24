using AdminUI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace AdminUI.Controllers
{
    public class BrandController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<BrandItemViewModel> data = new List<BrandItemViewModel>();
            using(HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(HeaderNames.Authorization, "Bearer " + Request.Cookies["token"]);
                using(var response = await client.GetAsync("https://localhost:7137/admin/api/Brands"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseStr = await response.Content.ReadAsStringAsync();
                        data = JsonConvert.DeserializeObject<List<BrandItemViewModel>>(responseStr);
                    }
                    else
                    {
                        return RedirectToAction("error", "home");
                    }
                   
                }
            } 
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BrandCreateViewModel brand)
        {
            using(HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(HeaderNames.Authorization, "Bearer " + Request.Cookies["token"]);
                StringContent content = new StringContent(JsonConvert.SerializeObject(brand), Encoding.UTF8, "application/json");
                using(var response = await client.PostAsync("https://localhost:7137/admin/api/Brands",content))
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ModelState.AddModelError("Name", "Name already taken");
                        return View();
                    }
                }
            }

            return RedirectToAction("index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            BrandEditViewModel data = new BrandEditViewModel();
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(HeaderNames.Authorization, "Bearer " + Request.Cookies["token"]);
                using (var response = await client.GetAsync("https://localhost:7137/admin/api/Brands/"+id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        return RedirectToAction("error", "home");

                    string responseStr = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<BrandEditViewModel>(responseStr);
                }
            }

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,BrandEditViewModel brand)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(HeaderNames.Authorization, "Bearer " + Request.Cookies["token"]);
                StringContent content = new StringContent(JsonConvert.SerializeObject(brand), Encoding.UTF8, "application/json");
                using (var response = await client.PutAsync("https://localhost:7137/admin/api/Brands/"+id, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ModelState.AddModelError("Name", "Name already taken");
                        return View();
                    }
                    else if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        return RedirectToAction("error", "home");
                    }
                }
            }

            return RedirectToAction("index");
        }
    }
}
