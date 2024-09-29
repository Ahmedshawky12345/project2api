using System.ComponentModel.DataAnnotations;

namespace project2api.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mangsername { get; set; }

        // relation with employee
        public ICollection<product>? products { get; set; }
    }
}
