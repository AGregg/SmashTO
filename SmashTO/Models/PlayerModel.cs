using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace SmashTO.Models
{
    public class PlayersContext : DbContext
    {
        public PlayersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<PlayerModel> Players { get; set; }
    }

    [Table("Players")]
    public class PlayerModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int PlayerId { get; set; }

        [Required]
        [Display(Name = "Player name")]
        public string PlayerName { get; set; }

        public int Rating { get; set; }
    }
}
