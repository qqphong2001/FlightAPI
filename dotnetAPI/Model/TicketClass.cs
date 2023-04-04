using System.ComponentModel.DataAnnotations;

namespace dotnetAPI.Model
{
    public class TicketClass
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; }


    
    }
}
