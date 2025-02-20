using System.ComponentModel.DataAnnotations;

namespace Portfi.Infrastructure.Models.Requests;

public class CreatePortfolioRequest
{
    [Required]
    public required string Biography { get; set; }

    [Required]
    public required string[] Names { get; set; }
}