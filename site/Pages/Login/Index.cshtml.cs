using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace site.Pages.Login
{
	public class IndexModel : PageModel
	{
		public IActionResult OnGet()
		{
			return Challenge(new AuthenticationProperties { RedirectUri = "/" }, "Spotify");
		}
	}
}