using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data;

public class OrderData
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { set; get; }

    public long ProductId { set; get; }
    public double TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; }
}