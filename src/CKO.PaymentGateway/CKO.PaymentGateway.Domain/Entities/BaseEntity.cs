namespace CKO.PaymentGateway.Domain.Entities;

public class BaseEntity
{
    internal BaseEntity()
    {
        CreatedAt = DateTime.UtcNow;
    }
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
}