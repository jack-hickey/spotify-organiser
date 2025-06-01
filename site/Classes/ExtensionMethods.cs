using System.Collections.Specialized;
using Microsoft.AspNetCore.Mvc;

namespace site.Classes
{
	public static class ExtensionMethods
	{
		public static string GetTopLevel(this IUrlHelper helper, HttpContext context)
		{
			Uri uri = new(context.Request.Scheme + "://" + context.Request.Host + helper.Content("~/"));

			string url = uri.ToString().ToLower();
			return url.EndsWith('/') ? url : url + '/';
		}

		public static Uri ChangeQueryParameter(this Uri uri, string name, object value)
		{
			NameValueCollection data = System.Web.HttpUtility.ParseQueryString(uri.Query);

			if (data[name] != null)
			{
				data.Set(name, value?.ToString());
			}
			else
			{
				data.Add(name, value?.ToString());
			}

			if (string.IsNullOrWhiteSpace(value?.ToString())) { data.Remove(name); }

			return new Uri($"{uri.GetLeftPart(UriPartial.Path)}{(data.AllKeys.Length > 0 ? "?" : "")}{string.Join("&", data.AllKeys.Select(x => x + "=" + System.Web.HttpUtility.UrlEncode(data[x])))}");
		}
	}
}