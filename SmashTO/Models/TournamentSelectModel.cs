using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmashTO.Models
{
    [Table("Players")]
    public class TournamentSelectModel
    {
        public IList<SwissBracket> FinishedBrackets { get; set; }
        public IList<SwissBracket> InProgressBrackets { get; set; }

        public TournamentSelectModel()
        {
            FinishedBrackets = new List<SwissBracket>();
            InProgressBrackets = new List<SwissBracket>();
        }
    }
}
