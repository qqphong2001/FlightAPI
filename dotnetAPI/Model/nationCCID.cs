using System.ComponentModel.DataAnnotations;

namespace dotnetAPI.Model
{
    public class nationCCID
    {
        [Key]
        public int Id { get; set; }

        public string firstName { get; set; }
        public string lastName { get; set; }
        public string dob { get; set; }
        public string address { get; set; }

        public string Area { get; set; }

    }
}
