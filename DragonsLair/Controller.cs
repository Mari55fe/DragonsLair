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
            return tournamentRepository; //
        }
        private List<Team> Scramble(List<Team> teams)
        {
            return teams;
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
            List<Team> scramble;
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
                    scramble = Scramble(teams).ToList();
                    Round newRound = new Round();
                    if (scramble.Count % 2 != 0) //vi tester med modulus om der er et lige eller ulige antal teams
                    {
                        if (numberOfRounds > 0)
                        {
                            oldFreeRider = lastRound.FreeRider;
                        }
                        else
                        {
                            oldFreeRider = null;
                        }
                        int i = 0; //i skal declares udefor while loppets scope
                        while (newFreeRider == oldFreeRider)
                        {
                            newFreeRider = scramble[i];
                            i++;
                        }

                        newRound.FreeRider = newFreeRider;
                        scramble.Remove(newFreeRider); // nu gøre vi sådan at den fejner new freerider fra scramble
                    }
                    for (int i = 0; i < scramble.Count; i += 2)
                    {
                        Match match = new Match();
                        match.FirstOpponent = scramble[i];
                        match.SecondOpponent = scramble[i + 1];
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
        



        public void SaveMatch(string tournamentName, int round, string winner)
        {
            Tournament t = tournamentRepository.GetTournament(tournamentName);
            Round r = t.GetRound(round);
            Match m = r.GetMatch(winner);
            if (m != null && m.Winner == null)
            {
                Team w = t.GetTeam(winner);
                Console.WriteLine("Kampen mellem " + m.FirstOpponent + " og " + m.SecondOpponent + " i runde 2 i turneringen " + tournamentName + " er nu afviklet. Vinderen blev " + winner + ".");
                m.Winner = w;
            }
            else
            {
                Console.WriteLine("Holdet " + winner + " kan ikke være vinder i runde 2, da holdet enten ikke deltager i runde 2 eller kampen allerede er registreret med en vinder.");
            }
        }
    }
}
