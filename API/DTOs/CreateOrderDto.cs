using System.ComponentModel.DataAnnotations;
using Core.Entities.OrderAggregate;

namespace API.DTOs;

public class CreateOrderDto
{
    [Required]
    public string CartId { get; set; } = string.Empty;

    [Required]
    public int DeliveryMethodId { get; set; }

    [Required]
    public ShippingAddressDto ShippingAddress { get; set; } = null!;

    [Required]
    public PaymentSummaryDto PaymentSummary { get; set; } = null!;
}
