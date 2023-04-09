using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Domain.Models.Common;

namespace Core.Domain.Models;

/// <summary>
/// Użytkownik aplikacji
/// </summary>
public class User : BaseEntity
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Login { get; set; }
    
    public string Password { get; set; }
    
}