using System.ComponentModel.DataAnnotations.Schema;

namespace SmashTO.Models
{
    [Table("SwissBrackets")]
    public class Match
    {
        public int TournamentId { get; set; }
        public bool Finalized { get; set; }
        public int WinnerId { get; set; }
        public int LoserId { get; set; }

        public Match()
        {
            Finalized = false;
            WinnerId = 0;
            LoserId = 0;
        }

        //public void Swap()
        //{
        //    var tempId = LoserId;
        //    var LoserId = WinnerId;
        //    WinnerId = tempId;
        //    Finalize();
        //}

        //public void Finalize()
        //{
        //    Finalized = true;
        //}
    }
}
