using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Models.Common;

/// <summary>
/// Klasa będąca bazą każdej encji.
/// </summary>
public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }
}