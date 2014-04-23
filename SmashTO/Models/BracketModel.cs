using System.Collections.Generic;

namespace SmashTO.Models
{
    public abstract class BracketModel
    {
        public abstract IList<MatchModel> Matches();  // a list of matches ordered by occurance
        public abstract IList<PlayerModel> Players();
        public abstract IList<ResultsModel> Results();
    }
}
