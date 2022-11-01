using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsEF.Models
{
    [Table("brands")] // That's specify the class is maped only to the brands database
    public class Brand
    {
        [Key] // specify as a key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Generate an id
        public int Id { get; set; }

        [MaxLength(100)]
        [Required] // means can't be null
        public string Name { get; set; }

        //In a brand we can have many cars, so we use a collection
        [NotMapped] // doesn't have to look in database !!!Navigation propery!!!
        // Whenever we use a navigation property, the method should be virtual
        public virtual ICollection<Car> Cars { get; set; } // ICollection at the end there is a foreign key relationship

        public Brand()
        {
            Cars = new HashSet<Car>();
        }
    
    
    }
}
