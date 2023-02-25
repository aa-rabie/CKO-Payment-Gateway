
namespace CKO.PaymentGateway.Domain.Entities
{
    /// <summary>
    /// represents a payment operation requested by specific merchant
    /// </summary>
    public class Payment : BaseEntity
    {
        /// <summary>
        /// Merchant ID
        /// </summary>
        public Guid MerchantId { get; set; }
        /// <summary>
        /// Card Number which will be used to pay
        /// </summary>
        public string CardNumber { get; set; }
        /// <summary>
        /// Card Type - for example "VISA"
        /// </summary>
        public string CardType { get; set; }
        /// <summary>
        /// Card CVV
        /// </summary>
        public string Cvv { get; set; }
        /// <summary>
        /// Card Expiry Month
        /// </summary>
        public int ExpiryMonth { get; set; }
        /// <summary>
        /// Card Expiry Year
        /// </summary>
        public int ExpiryYear { get; set; }
        /// <summary>
        /// Amount to pay - should be non-negative
        /// </summary>
        public double Amount { get; set; }
        /// <summary>
        /// Currency - for example "GBP" or "EURO"
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// True if Payment completed successfully otherwise False
        /// </summary>
        public bool Succeeded { get; set; }
        
    }
}
