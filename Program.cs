using Auth0.AspNetCore.Authentication;
using MellonThinClient.Data;
using MellonThinClient.Services.ToDosApiService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration["Auth0:Domain"];
    options.ClientId = builder.Configuration["Auth0:ClientId"];
    options.ClientSecret = builder.Configuration["Auth0:ClientSecret"];
    options.Scope = "openid profile read:todos write:todos";

}).WithAccessToken(options =>
{
    options.Audience = builder.Configuration["Auth0:Audience"];
    options.UseRefreshTokens = true;
    options.Events = new Auth0WebAppWithAccessTokenEvents
    {
        OnMissingRefreshToken = async (context) =>
        {
            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var authenticationProperties = new LogoutAuthenticationPropertiesBuilder().WithRedirectUri("/").Build();
            await context.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        }
    };
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<TokenHandler>();

builder.Services.AddHttpClient("ToDosAPI", client => client.BaseAddress = new Uri(builder.Configuration["ToDosAPIBaseUrl"]))
    .AddHttpMessageHandler<TokenHandler>();

builder.Services.AddScoped<IToDosRepository, ToDosRepository>();
builder.Services.AddScoped<IToDosApiClient, ToDosApiClient>();


builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
    .CreateClient("ToDosAPI"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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

