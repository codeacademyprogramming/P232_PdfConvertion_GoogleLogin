using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialLogin.DAL;
using SocialLogin.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredLength = 6;
}).AddDefaultTokenProviders().AddEntityFrameworkStores<DataContext>();


builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
                .AddCookie(options =>
                {
                    options.LoginPath = "/account/google-login";
                })
    .AddGoogle(opt =>
    {
        //opt.ClientId = builder.Configuration.GetSection("Socials:Google:Id").Value;
        //opt.ClientSecret = builder.Configuration.GetSection("Socials:Google:Secret").Value;
        opt.ClientId = "1010705384285-8m34q9pqkdd7ckhmuvivskcaacvjj8jj.apps.googleusercontent.com";
        opt.ClientSecret = "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";
        opt.SignInScheme = IdentityConstants.ExternalScheme;
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
