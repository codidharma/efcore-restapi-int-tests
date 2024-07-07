using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;
public class ExpenseCategory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime DateModified { get; set; }
}
