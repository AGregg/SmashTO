using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmashTO.Models
{
    [Table("Players")]
    public class PlayerModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int PlayerId { get; set; }

        [Required]
        [Display(Name = "Player name")]
        public string PlayerName { get; set; }

        public int Rating { get; set; }
    }
}
