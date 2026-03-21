namespace IASales.Api.Entities;

public class Lead
{
  public Guid Id {get; set;}
  public string Name {get; set;} = string.Empty;

  public string Email { get; set; } = string.Empty;

  public string Phone {get; set; } = string.Empty;

  public string Source {get; set; } = string.Empty;

  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}