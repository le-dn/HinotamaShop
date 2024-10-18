using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class PaymentSummaryDto
{
    public int Last4 { get; set; }

    [Required]
    public string Brand { get; set; } = string.Empty;

    public int ExpMonth { get; set; }

    public int ExpYear { get; set; }
}
