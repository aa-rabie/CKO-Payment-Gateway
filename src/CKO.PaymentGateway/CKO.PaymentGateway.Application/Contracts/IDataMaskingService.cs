namespace CKO.PaymentGateway.Application.Contracts;

public interface IDataMaskingService
{
    /// <summary>
    /// Mask CardNumber except last 4 digits
    /// </summary>
    /// <param name="cardNumber">Card Number to be masked</param>
    /// <returns>masked card number</returns>
    string MaskCardNumber(string cardNumber);
    /// <summary>
    /// Mask Cvv using predefined character
    /// </summary>
    /// <param name="cvv">Cvv value to be masked</param>
    /// <returns>masked Cvv</returns>
    string MaskCardCvv(string cvv);
}