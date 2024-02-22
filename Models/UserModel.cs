using Jan6.Validations;
using System.ComponentModel.DataAnnotations;

namespace Jan6.Models;

public class UserModel
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(maximumLength: 20, ErrorMessage = "Must to indicate User Name")]
    [FirstCapitalAttribute]
    public string Name { get; set; }
    public int Age { get; set; }
}