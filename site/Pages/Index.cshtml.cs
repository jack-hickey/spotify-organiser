using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace site.Pages
{
	public class IndexModel : PageModel
	{
		public string Token { get; set; } = "";

		public async Task<IActionResult> OnGet()
		{
			Token = await HttpContext.GetTokenAsync("access_token") ?? "";
			return Page();
		}

		public IActionResult OnGetLogin()
		{
			return Challenge(new AuthenticationProperties(), "Spotify");
		}
	}
}