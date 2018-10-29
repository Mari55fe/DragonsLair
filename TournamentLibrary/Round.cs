using System.Collections.Generic;

namespace TournamentLib
{
    public class Round
    {
        private List<Match> matches = new List<Match>();
        private Team freeRider;
        public Team FreeRider{ get; set;}

        public void AddMatch(Match m)
        {
            matches.Add(m);
        }

        public Match GetMatch(string teamName1, string teamName2)
        {
            // TODO: Implement this method
            Match match = new Match();
            foreach (Match m in matches)
            {
                if (m.FirstOpponent.ToString() == teamName1 && m.SecondOpponent.ToString() == teamName2)
                {
                    match = m;
                }
                
            }
            return match;
        }

        public bool IsMatchesFinished()
        {
            for (int i = 0; i < matches.Count; i++)
            {
                if (matches[i].Winner == null)
                {
                    return false;
                }
                
            }
            

            return true;
        }

        public List<Team> GetWinningTeams()
        {

            List<Team> WinningTeams = new List<Team>();
            for (int i = 0; i < matches.Count; i++)
            {
                WinningTeams.Add(matches[i].Winner);
            }
            return WinningTeams;
            
            
        }

        public List<Team> GetLosingTeams()
        {
            List<Team> LosingTeams = new List<Team>();
            List<Team> WinningTeams = new List<Team>();
            foreach (var match in matches)
            {
                if (match.Winner == match.FirstOpponent)
                {
                    WinningTeams.Add(match.FirstOpponent);
                    LosingTeams.Add(match.SecondOpponent);
                }
                else
                {
                    LosingTeams.Add(match.FirstOpponent);
                    WinningTeams.Add(match.SecondOpponent);
                }
            }
            
            return LosingTeams;
        }
    }
}
