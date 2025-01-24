namespace Aretech.Application.SeedWork
{
	public class ApiResponse<T>
	{
		/// <summary>
		/// Indicates whether the API operation was successful (true: success, false: failure)
		/// </summary>
		public bool Success { get; set; }

		/// <summary>
		/// Message describing the result of the operation (success or error message)
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// The data returned by the API (generic type)
		/// </summary>
		public T Data { get; set; }

		/// <summary>
		/// Details of errors (if any)
		/// </summary>
		public List<string> Errors { get; set; }

		/// <summary>
		/// Default constructor for ApiResponse
		/// </summary>
		public ApiResponse()
		{
			Errors = new List<string>();
		}

		/// <summary>
		/// Creates a successful API response
		/// </summary>
		/// <param name="data">The data to be returned</param>
		/// <param name="message">The success message (default: "Operation completed successfully.")</param>
		/// <returns>An instance of ApiResponse</returns>
		public static ApiResponse<T> SuccessResponse(T data, string message = "İşlem başarıyla tamamlandı.")
		{
			return new ApiResponse<T>
			{
				Success = true,
				Message = message,
				Data = data,
				Errors = null
			};
		}

		/// <summary>
		/// Creates an error response with a list of error messages
		/// </summary>
		/// <param name="errors">A list of error messages</param>
		/// <param name="message">A descriptive error message (default: "Operation failed.")</param>
		/// <returns>An instance of ApiResponse</returns>
		public static ApiResponse<T> ErrorResponse(List<string> errors, string message = "İşlemde hata oluştu.")
		{
			return new ApiResponse<T>
			{
				Success = false,
				Message = message,
				Data = default,
				Errors = errors
			};
		}

		/// <summary>
		/// Creates an error response with a single error message
		/// </summary>
		/// <param name="error">The error message</param>
		/// <param name="message">A descriptive error message (default: "Operation failed.")</param>
		/// <returns>An instance of ApiResponse</returns>
		public static ApiResponse<T> ErrorResponse(string error, string message = "İşlemde hata oluştu.")
		{
			return new ApiResponse<T>
			{
				Success = false,
				Message = message,
				Data = default,
				Errors = new List<string> { error }
			};
		}
	}
}