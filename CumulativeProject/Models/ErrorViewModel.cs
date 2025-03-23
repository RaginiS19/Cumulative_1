namespace CumulativeProject.Models
{
    /// <summary>
    /// Represents the error view model, which contains details about errors.
    /// Includes properties for the request ID and a flag to determine whether the request ID should be shown.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Gets or sets the unique request ID for the error (nullable).
        /// </summary>
        public string? RequestId { get; set; }
        /// <summary>
        /// Gets a value indicating whether the request ID should be displayed.
        /// Returns true if the RequestId is not null or empty; otherwise, false.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
