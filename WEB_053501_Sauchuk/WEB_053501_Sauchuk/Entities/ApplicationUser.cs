using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace WEB_053501_Sauchuk.Entities;

public class ApplicationUser : IdentityUser
{
    [NotMapped]
    public byte[]? Image { get; set; }
    public string? ContentType { get; set; }
    
}