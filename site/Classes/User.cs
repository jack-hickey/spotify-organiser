using System.Text.Json.Serialization;

namespace site.Classes
{
	public class User
	{
		[JsonPropertyName("id")]
		public string ID { get; set; } = "";

		[JsonPropertyName("display_name")]
		public string Name { get; set; } = "";

		[JsonPropertyName("images")]
		public UserImage[] Images { get; set; } = [];

		public class UserImage
		{
			[JsonPropertyName("url")]
			public string URL { get; set; } = "";
		}
	}
}