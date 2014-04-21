using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web.Security;

namespace SmashTO.Models
{
    [Table("SwissBrackets")]
    public class SwissModel : BracketModel
    {
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
