using System.ComponentModel.DataAnnotations;

namespace ProductManagementAPI.Models;

public class Product : IValidatableObject
{
    public int Id { get; set; }

    [Required, StringLength(80)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(80)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public double Price { get; set; }
    public double Stock { get; set; }
    public DateTime? DeletedAt { get; set; }
    public DateTime CreatedAt { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (this.Stock <= 0) yield return new ValidationResult("The stock must be greater than zero",
            new[]
            {nameof(this.Stock)}
            );

        if (this.Price < 0) yield return new ValidationResult("The price cannot be negative ");
    }
}
