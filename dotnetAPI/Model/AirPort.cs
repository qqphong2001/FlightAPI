using System.ComponentModel.DataAnnotations;

namespace dotnetAPI.Model
{
    public class AirPort
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public string IATA { get; set; }

        public string Gates { get; set; }
    }
}
