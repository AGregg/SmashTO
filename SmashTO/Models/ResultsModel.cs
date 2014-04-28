using System.Collections.Generic;

namespace SmashTO.Models
{
    public class ResultsModel
    {
        public string TournamentName { get; set; }
        
        public IList<Result> Results { get; set; }

        public ResultsModel()
        {
            Results = new List<Result>();
        }
    }
}
