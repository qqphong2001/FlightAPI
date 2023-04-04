using System.ComponentModel.DataAnnotations;

namespace dotnetAPI.Model
{
    public class AirLine
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Logo { get; set; }

    }
}
