using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmashTO.Models
{
    public abstract class BracketModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TournamentId { get; set; }

        public String Name { get; set; }
        public bool IsFinished { get; set; }

        public BracketModel()
        {
            IsFinished = false;
        }

        public abstract IList<MatchModel> Matches();  // a list of matches ordered by occurance
        public abstract IList<PlayerModel> Players();  // a list of players that were in the tournament
        public abstract ResultsModel Results();  // results of the tournament
    }
}
