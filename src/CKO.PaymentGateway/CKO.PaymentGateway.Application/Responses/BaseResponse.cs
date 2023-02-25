namespace CKO.PaymentGateway.Application.Responses
{
    /// <summary>
    /// provide basic functionality for Responses used by Handlers 
    /// </summary>
    public class BaseResponse
    {
        public BaseResponse()
        {
            Success = true;
        }

        public BaseResponse(List<string> validationErrors)
        {
            Success = false;
            ValidationErrors = validationErrors;
        }

        public BaseResponse(string error)
        {
            Success = false;
            ValidationErrors.Add(error);
        }

        /// <summary>
        /// its value should be True unless there are any validation errors 
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Stores List of validation errors if any
        /// </summary>
        public List<string> ValidationErrors { get; set; } = new List<string>();
    }
}
