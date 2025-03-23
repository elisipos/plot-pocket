using PlotPocket.Server.Models.Entities;

namespace Server.Models.Dtos;

public class UserDto{
  public required string Id { get; set; }
  public required string Email { get; set; }
  public virtual ICollection<Show> Shows { get; set; } = new List<Show>();
}