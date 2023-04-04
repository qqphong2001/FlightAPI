using System.ComponentModel.DataAnnotations;

namespace dotnetAPI.Model
{
    public class Status
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
