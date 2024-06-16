using System.Text.Json.Serialization;
using ConsoleApp7Plus.Enumerations;

namespace ConsoleApp7Plus.Models
{
	/// <summary>Basic user data class used when creating new user</summary>
	public class UserBasicData
	{
		/// <summary>First name of the user</summary>
		[JsonPropertyName("first")]
		public required string FirstName { get; set; }

		/// <summary>Last name of the user</summary>
		[JsonPropertyName("last")]
		public required string LastName { get; set; }

		/// <summary>Age of the user</summary>
		[JsonPropertyName("age")]
		public int Age { get; set; }

		/// <summary>Gender of the user, valid values are <see cref="Gender.F"/>, <see cref="Gender.M"/>, <see cref="Gender.T"/> and <see cref="Gender.Y"/></summary>
		[JsonPropertyName("gender")]
		public Gender Gender { get; set; }
	};

	/// <summary>Full user data class used when getting a user record</summary>
	public class User : UserBasicData
	{
		/// <summary>ID of the user record, this is readonly and only added upon creation</summary>
		[JsonPropertyName("id")]
		public required string Id { get; init; }
	};

	/// <summary>Aggregation data at a specific age</summary>
	public class UserAggreation
	{
		/// <summary>Targeted age for aggreation value</summary>
		[JsonPropertyName("age")]
		public required int Age { get; set; }

		/// <summary>Number of female having the same provided age</summary>
		[JsonPropertyName("female")]
		public required int Female { get; set; }

		/// <summary>Number of male having the same provided age</summary>
		[JsonPropertyName("male")]
		public required int Male { get; set; }
	};
}

