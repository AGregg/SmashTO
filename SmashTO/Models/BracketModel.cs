using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace SmashTO.Models
{
    public abstract class BracketModel
    {
        public abstract IList<MatchModel> Matches();  // a list of matches ordered by occurance
        public abstract IList<PlayerModel> Players();
        public abstract IList<ResultsModel> Results();
    }
}
