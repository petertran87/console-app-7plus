namespace ConsoleApp7Plus.Utils
{
	public static class CommonUtils
	{
		public static bool IsDevelopment()
		{
			string environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development";
			return environment == "Development";
		}
	}
}