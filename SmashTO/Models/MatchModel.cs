using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace SmashTO.Models
{
    public class MatchModel
    {
        public int BracketId { get; set; }
        public int WinnerId { get; set; }
        public int LoserId { get; set; }
    }
}
