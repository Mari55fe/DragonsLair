﻿using System.Collections.Generic;

namespace TournamentLib
{
    public class TournamentRepo
    {
        private List<Tournament> tournaments = new List<Tournament>();
        public void AddTurnament(Tournament Name)
        {
            
            tournaments.Add(Name);
        }

        private Tournament winterTournament = new Tournament("Vinter Turnering");

        public Tournament GetTournament(string name)
        {
            if (name == "Vinter Turnering")
            {
                return winterTournament;
            }
            return null;
        }
        
    }
} 
