using System.Net;
using ConsoleApp7Plus.Models;

namespace ConsoleApp7Plus.Exceptions
{
	public class CustomRequestException : Exception
	{
		/// <summary>Base constructor</summary>
		public CustomRequestException() : base() { }

		/// <summary>Extend the message context with invalid user ID</summary>
		/// <param name="id">Value of the invalid ID</param>
		public CustomRequestException(string route, HttpStatusCode? statusCode, string? errorMsg) : base(
			$"Encountered an error when retrieving data from route {route} with status code {statusCode?.ToString() ?? "unknown"}" +
			$"{(string.IsNullOrEmpty(errorMsg) ? " without context." : $". Reason: {errorMsg}")}"
		)
		{ }
	}

	/// <summary>Exception used when trying to find data with an invalid ID</summary>
	public class InvalidUserIdException : Exception
	{
		/// <summary>Base constructor</summary>
		public InvalidUserIdException() : base() { }

		/// <summary>Extend the message context with invalid user ID</summary>
		/// <param name="id">Value of the invalid ID</param>
		public InvalidUserIdException(int id) : base($"Cannot find a record for provided user ID {id}") { }
	}

	/// <summary>Exception used when an attempt to perform an operation results in failure</summary>
	/// <typeparam name="T">Data type associated with the exception</typeparam>
	public class InvalidRequestOperationException<T> : Exception
	{
		private readonly T? _Data;
		private readonly string _Operation;
		private readonly string _Reason;

		/// <summary>Base constructor</summary>
		public InvalidRequestOperationException(string reason, string? operation) : base()
		{
			_Operation = operation ?? "unknown";
			_Reason = reason;
		}

		/// <summary>Extend the message context with invalid user ID</summary>
		/// <param name="id">Value of the invalid ID</param>
		public InvalidRequestOperationException(string reason, T? data, string? operation) : base()
		{
			_Data = data;
			_Operation = operation ?? "unknown";
			_Reason = reason;
		}

		public override string Message
		{
			get
			{
				if (_Data != null)
				{
					if (_Data is User user)
					{
						return $"Invalid {_Operation.ToLower()} operation: Cannot perform operation for user {user.FirstName} {user.LastName} for reason \"{_Reason}\"";
					}
				}

				return $"Invalid {_Operation.ToLower()} operation for reason \"{_Reason}\"";
			}
		}
	}
}

