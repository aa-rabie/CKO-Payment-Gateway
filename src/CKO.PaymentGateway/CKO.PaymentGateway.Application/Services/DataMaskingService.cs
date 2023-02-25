using CKO.PaymentGateway.Application.Contracts;

namespace CKO.PaymentGateway.Application.Services;

public class DataMaskingService : IDataMaskingService
{
    private const char Mask = '*';
    /// <summary>
    /// mask card-number string except last 4 characters 
    /// </summary>
    /// <param name="cardNumber">card number to be masked</param>
    /// <returns>masked card number</returns>
    public string MaskCardNumber(string cardNumber)
    {
        if (string.IsNullOrEmpty(cardNumber))
            return cardNumber;

        cardNumber = cardNumber.Trim();

        if(cardNumber.Length <= 4)
            return new string(Mask, cardNumber.Length);

        var lastFourDigits = cardNumber.Substring(cardNumber.Length - 4, 4);
        var maskedString = new string(Mask, cardNumber.Length - 4);
        return $"{maskedString}{lastFourDigits}";
    }

    /// <summary>
    /// replace Cvv value with masked string
    /// </summary>
    /// <param name="cvv">cvv value to be masked</param>
    /// <returns>masked cvv value</returns>
    public string MaskCardCvv(string cvv)
    {
        if (string.IsNullOrEmpty(cvv))
            return cvv;

        cvv = cvv.Trim();
        return new string(Mask, cvv.Length);
    }
}