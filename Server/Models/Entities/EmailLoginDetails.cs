namespace Server.Models.Entities;

public class EmailLoginDetails {
  public required string Email { get; set; }
  public string? Password { get; set; }
}