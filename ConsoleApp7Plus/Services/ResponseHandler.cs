using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using ConsoleApp7Plus.Abstraction;
using ConsoleApp7Plus.Enumerations;
using ConsoleApp7Plus.Exceptions;
using ConsoleApp7Plus.Models;
using Microsoft.Extensions.Logging;

namespace ConsoleApp7Plus.Services
{
	public class HandlerApiJsonResponse : ResponseHandler
	{
		private readonly ILogger<HandlerApiJsonResponse> _logger;

		public HandlerApiJsonResponse(ILogger<HandlerApiJsonResponse> logger)
		{
			_logger = logger;
		}

		public override async Task<T> ExecuteAsync<T>(ApiCall func, string endpoint)
		{
			try
			{
				HttpResponseMessage response = await func();

				// Allow throwing exception when status is not OK
				response.EnsureSuccessStatusCode();

				// Read response content as a stream
				using var responseStream = await response.Content.ReadAsStreamAsync();

				// Deserialise JSON from stream
				var options = new JsonSerializerOptions { Converters = { new JsonStringEnumConverter<Gender>() } };

				var data = await JsonSerializer.DeserializeAsync<T>(responseStream, options) ?? throw new Exception("No JSON file could be created");

				return data;
			}
			// For 404 NOT FOUND error
			catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.NotFound)
			{
				CustomRequestException ex = new(endpoint, e.StatusCode, e.Message);
				_logger.LogTrace(ex, "Could not retrieve user list");

				throw ex;
			}
			// From 500 INTERNAL SERVER ERROR error onwards
			catch (HttpRequestException e) when (e.StatusCode >= HttpStatusCode.InternalServerError)
			{
				CustomRequestException ex = new(endpoint, e.StatusCode, e.Message);
				_logger.LogCritical(e, "Critical server error");

				throw ex;
			}
			// For authentication or authorisation issues
			catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.Unauthorized || e.StatusCode == HttpStatusCode.Forbidden)
			{
				CustomRequestException ex = new(endpoint, e.StatusCode, e.Message);
				_logger.LogWarning(e, "Failed authentication attempt");

				throw ex;
			}
			catch (JsonException e)
			{
				InvalidRequestOperationException<List<User>> ex = new(e.Message, "parsing JSON");
				_logger.LogCritical(ex, "JSON response parsing error");

				throw ex;
			}
			catch (Exception e)
			{
				_logger.LogError(e, "Encountered error at endpoint \"/{endpoint}\"", endpoint);
				throw;
			}
		}
	}
}