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
    public class ResultsModel
    {
        public int Placing { get; set; }
        public int Player { get; set; }
        public int Score { get; set; }
    }
}
