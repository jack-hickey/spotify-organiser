using System.Text.Json.Serialization;

namespace site.Spotify
{
	public class Artist
	{
		[JsonPropertyName("name")]
		public string Name { get; set; } = "";
	}
}