using System.Data.Entity;

namespace SmashTO.Models
{
    public class TournamentContext : DbContext
    {
        public TournamentContext()
            : base("DefaultConnection")
        {
            this.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<PlayerModel> Players { get; set; }
        public DbSet<SwissBracket> SwissBrackets { get; set; }
        public DbSet<SwissRound> SwissRounds { get; set; }
        public DbSet<SwissMatch> SwissMatches { get; set; }
    }
}
