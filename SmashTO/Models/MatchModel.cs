using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmashTO.Models
{
    [Table("Matches")]
    public class MatchModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MatchId { get; set; }
        
        //[ForeignKey("Player1")]
        public int Player1Id { get; set; }
        //public virtual PlayerModel Player1 { get; set; }

        public int Player2Id { get; set; }
        //public virtual PlayerModel Player2 { get; set; }

        
        public int WinnerId { get; set; }
        //public virtual PlayerModel Winner { get; set; }

        public MatchModel()
        {
            Player1Id = 0;
            Player2Id = 0;
            WinnerId = 0;
        }

        public MatchModel(int byeId)
        {
            Player1Id = byeId;
            Player2Id = -1;
            WinnerId = byeId;
        }

        public MatchModel(int p1, int p2)
        {
            Player1Id = p1;
            Player2Id = p2;
            WinnerId = 0;
        }
    }
}
