using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace SmashTO.Models
{
    [Table("Players")]
    public class PlayerSelectModel
    {
        public IEnumerable<PlayerModel> Players { get; set; }
        public TournamentFormat Format { get; set; }
    }
}
