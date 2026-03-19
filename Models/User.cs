using System.ComponentModel.DataAnnotations;

namespace AspnetCoreMvcFull.Models
{
  public class User
  {
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
  }
}
