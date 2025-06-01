using System.Text.Json.Serialization;

namespace site.Spotify
{
	public class Track
	{
		[JsonPropertyName("name")]
		public string Name { get; set; } = "";
	}
}