using System;
using System.Collections.Generic;
using System.Linq;
using TournamentLib;

namespace DragonsLair
{
    public class Controller
    {
        
        private TournamentRepo tournamentRepository = new TournamentRepo();

        public void ShowScore(string tournamentName)
        {
            Tournament tor = tournamentRepository.GetTournament(tournamentName);
            Team[] teams = tor.GetTeams().ToArray();
            int[] scores = new int[teams.Length];
            for (int i = 0; i < tor.GetNumberOfRounds(); i++)
            {
                Round round = tor.GetRound(i);
                List<Team> winningTeams = round.GetWinningTeams();
                for (int teamI = 0; teamI < teams.Length; teamI++)
                {
                    for (int winningTeamI = 0; winningTeamI < winningTeams.Count; winningTeamI++)
                    {
                        if (teams[teamI].Name == winningTeams[winningTeamI].Name)
                        {
                            scores[teamI]++;
                        }
                    }
                }
            }
            for (int num = scores.Max(); num >= 0; num--)
            {
                for (int i = 0; i < teams.Length; i++)
                {
                    if (scores[i] == num)
                    {
                        Console.WriteLine($"team: {teams[i]}, Score: {scores[i]}");
                    }
                }
            }
            /*
             * TODO: Calculate for each team how many times they have won
             * Sort based on number of matches won (descending)
             */
            Console.WriteLine("\n\n");
        }

        public TournamentRepo GetTournamentRepository()
        {
            throw new NotImplementedException();
        }

        public void ScheduleNewRound(string tournamentName, bool printNewMatches = true)
        {
            bool isRoundFinished;
            Team oldFreeRider;
            Team newFreeRider = null;
            Round lastRound;
            List<Team> teams = new List<Team>();
            Tournament t = tournamentRepository.GetTournament(tournamentName);
            int numberOfRounds = t.GetNumberOfRounds();
            if(numberOfRounds == 0)
            {
                lastRound = null;
                isRoundFinished = true;
            }
            else
            {
                lastRound = t.GetRound(numberOfRounds - 1);
                isRoundFinished = lastRound.IsMatchesFinished();
            }
            if(isRoundFinished == true)
            {
                if (lastRound == null)
                {
                    teams = t.GetTeams();
                }
                else
                {
                    teams = lastRound.GetWinningTeams();
                    if(lastRound.FreeRider != null)
                    {
                        teams.Add(lastRound.FreeRider);
                    }
                }
                if (teams.Count > 1)
                {
                    Round newRound = new Round();
                    if (teams.Count % 2 != 0) //vi tester med modulus om der er et lige eller ulige antal teams
                    {
                        if (numberOfRounds > 0)
                        {
                            oldFreeRider = lastRound.FreeRider;
                        }
                        else
                        {
                            oldFreeRider = null;
                        }
                        while (newFreeRider == oldFreeRider)
                        {
                            int i = 0;
                            newFreeRider = teams[i];
                            i++;
                        }

                        newRound.FreeRider = newFreeRider;
                    }
                    for (int i = 0; i == teams.Count; i += 2)
                    {
                        Match match = new Match();
                        match.FirstOpponent = teams[i];
                        match.SecondOpponent = teams[i + 1];
                        newRound.AddMatch(match);
                    }
                    t.AddRound(newRound);
                }
                else
                {
                    throw new Exception("TournamentIsFinshed");
                }
            }
            else
            {
                throw new Exception("TournamentIsFinished");
            }
        }

        public void SaveMatch(string tournamentName, int roundNumber, string team1, string team2, string winningTeam)
        {
            // Do not implement this method
        }
    }
}
