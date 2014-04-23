using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmashTO.Models
{
    [Table("SwissBrackets")]
    public class SwissModel : BracketModel
    {
        public IList<SwissRound> Rounds { get; set; }

        public override IList<MatchModel> Matches()
        {
            throw new NotImplementedException();
        }

        public override IList<PlayerModel> Players()
        {
            throw new NotImplementedException();
        }

        public override IList<ResultsModel> Results()
        {
            throw new NotImplementedException();
        }
    }
}
