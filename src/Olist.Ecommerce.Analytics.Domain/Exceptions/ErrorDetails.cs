using System.Text.Json;

namespace Olist.Ecommerce.Analytics.Domain.Exceptions
{
    /// <summary>
    /// Error details model.
    /// </summary>
    public class ErrorDetails
    {
        /// <summary>
        /// Error status code.
        /// </summary>
        public int StatusCode { get; set; }
        
        /// <summary>
        /// Error message.
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// Serialize error details to JSON.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}