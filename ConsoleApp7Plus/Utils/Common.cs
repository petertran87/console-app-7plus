using System.Text.Json;

namespace ConsoleApp7Plus.Utils
{
	public static class CommonUtils
	{
		public static bool IsDevelopment()
		{
			string environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development";
			return environment == "Development";
		}

		public static async Task<T> ParseJsonStream<T>(Stream dataStream, JsonSerializerOptions? options = null)
		{
			if (options != null)
			{
				return await JsonSerializer.DeserializeAsync<T>(dataStream, options) ?? throw new Exception("No JSON file could be created");
			}

			return await JsonSerializer.DeserializeAsync<T>(dataStream) ?? throw new Exception("No JSON file could be created");
		}

		public static string StringifyJson<T>(T data, JsonSerializerOptions? options = null)
		{
			return JsonSerializer.Serialize(data, options);
		}
	}
}