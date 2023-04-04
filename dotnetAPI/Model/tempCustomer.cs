using System.ComponentModel.DataAnnotations;

namespace dotnetAPI.Model
{
    public class tempCustomer
    {
        [Key]
        public int Id { get; set; }

        public string phone { get; set; }

        public string email { get; set; }

        public int nationCCIDID { get; set; }

    }
}
