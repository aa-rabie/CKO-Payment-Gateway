namespace CKO.PaymentGateway.BankSimulator.Models;

/// <summary>
/// Internal Model used to store accepted cards' data in Bank Simulator
/// </summary>
internal record CardEntry(string CardNumber, string Cvv, int ExpiryMonth, int ExpiryYear, string CardType);