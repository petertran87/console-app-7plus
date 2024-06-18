using ConsoleApp7Plus.Exceptions;
using ConsoleApp7Plus.Models;

namespace ConsoleApp7Plus.Abstraction
{
	/// <summary>An abstract class to define all available methods in an API interaction class</summary>
	/// <typeparam name="T">Type associated to this class, can be <see cref="User"/>...</typeparam>
	public abstract class ApiInteraction<T, TDerived> where TDerived : T
	{
		public readonly string _Endpoint;

		/// <summary>Base constructor for abstract class to scaffold an API interaction class</summary>
		public ApiInteraction(string endpoint)
		{
			_Endpoint = endpoint;
		}

		/// <summary>Method to retrive a single data point based on ID</summary>
		/// <typeparam name="TDerived">This type must be the derived type of the base type specified for the class</typeparam>
		/// <returns>Full name of the user, will throw <see cref="InvalidUserIdException"/> if not found</returns>
		/// <exception cref="InvalidUserIdException"/>
		public abstract Task<TDerived> GetRecordById(int id);

		/// <summary>Method to retrive all available data from provided endpoint</summary>
		/// <typeparam name="TDerived">This type must be the derived type of the base type specified for the class</typeparam>
		/// <returns>A list of available data where possible.</returns>
		/// <exception cref="InvalidRequestOperationException{TBase}"/>
		public abstract Task<List<TDerived>> GetRecords();

		/// <summary>Method to create a new record</summary>
		/// <param name="data">Data needed to use when creating the data, this </param>
		/// <returns>ID of the newly create record</returns>
		/// <exception cref="InvalidRequestOperationException{TBase}"/>
		public abstract Task<int> CreateRecord(T data);

		/// <summary>Method to create</summary>
		/// <param name="data">Data needed to use when creating the data</param>
		/// <typeparam name="TDerived">This type must be the derived type of the base type specified for the class</typeparam>
		/// <remarks>The type used for the argument must be a derived type from base type provided in the class</remarks>
		/// <returns>No data is returned for successful operation</returns>
		/// <exception cref="InvalidRequestOperationException{TDerived}"/>
		public abstract Task UpdateRecord(TDerived data);
	}
}

