using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsEF.Models
{
    [Table("cars")]
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("car_id", TypeName = "int")] // specify the column
        public int Id { get; set; }
        [MaxLength(100)]
        [Required]
        public string Model { get; set; }
        public int? BestPrice { get; set; }
        [ForeignKey(nameof(Brand))] // refer to the brand
        public int BrandId { get; set; }

        [NotMapped]
        public virtual Brand Brand { get; set; }
    }
}
