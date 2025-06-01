using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

string[] spotifyScopes = [
	"user-library-read",
	"playlist-read-private",
	"user-read-private",
	"playlist-modify-public",
	"playlist-modify-private"
];

builder.Services.AddAuthentication(options =>
{
	options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = "Spotify";
})
.AddCookie()
.AddOAuth("Spotify", options =>
{
	options.ClientId = Environment.GetEnvironmentVariable("SPOTIFY_CLIENT_ID") ?? "";
	options.ClientSecret = Environment.GetEnvironmentVariable("SPOTIFY_CLIENT_SECRET") ?? "";
	options.CallbackPath = "/oauth";

	options.AuthorizationEndpoint = "https://accounts.spotify.com/authorize";
	options.TokenEndpoint = "https://accounts.spotify.com/api/token";
	options.UserInformationEndpoint = "https://api.spotify.com/v1/me";

	foreach (string scope in spotifyScopes)
	{
		options.Scope.Add(scope);
	}

	options.SaveTokens = true;
	options.ClaimActions.MapJsonKey(ClaimTypes.Name, "display_name");

	options.Events = new OAuthEvents
	{
		OnCreatingTicket = async context =>
		{
			HttpRequestMessage request = new(HttpMethod.Get, context.Options.UserInformationEndpoint);
			request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", context.AccessToken);

			HttpResponseMessage response = await context.Backchannel.SendAsync(request);
			response.EnsureSuccessStatusCode();

			string stringResponse = await response.Content.ReadAsStringAsync();
			context.Identity?.AddClaim(new Claim("spotify_user", stringResponse));

			JsonDocument user = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
			context.RunClaimActions(user.RootElement);
		}
	};
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.MapRazorPages();

app.Run();
