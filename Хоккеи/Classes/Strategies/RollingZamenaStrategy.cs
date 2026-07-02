using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Хоккеи.Classes.Managers;
using Хоккеи.Classes.Players;
using Хоккеи.Classes.Teams;

namespace Хоккеи.Classes.Strategies
{
    public class RollingZamenaStrategy : IZamenaStrategy
    {
        private Int32 CurrentDefenderPairIndex { get; set; }
        private Int32 CurrentForwardTripletIndex { get; set; }
        private Int32 LastSubstitutionMinute { get; set; }
        private Int32 LastFullChangeMinute { get; set; }
        private Boolean IsInAttackMode { get; set; }

        public RollingZamenaStrategy()
        {
            Reset();
        }
        public void Reset()
        {
            CurrentDefenderPairIndex = 0;
            CurrentForwardTripletIndex = 0;
            LastSubstitutionMinute = 0;
            LastFullChangeMinute = 0;
            IsInAttackMode = false;
        }
        public void UpdateLine(Team team, TimeManager timeManager)
        {
            Int32 currentMinute = timeManager.CurrentMinute;

            if (timeManager.IsLastMinuteOfPeriod())
            {
                if (!IsInAttackMode)
                {
                    team.SetAttackLine();
                    IsInAttackMode = true;
                }
                return;
            }

            if (IsInAttackMode)
            {
                IsInAttackMode = false;
                team.SetStandardLine();
            }

            if (currentMinute <= 2)
            {
                return;
            }

            if (currentMinute - LastFullChangeMinute >= 5)
            {
                PerformFullLineChange(team);
                LastFullChangeMinute = currentMinute;
                LastSubstitutionMinute = currentMinute;
                return;
            }

            if (currentMinute - LastSubstitutionMinute >= 1)
            {
                PerformSinglePlayerChange(team);
                LastSubstitutionMinute = currentMinute;
            }
        }

        private void ApplyStandardLine(Team team)
        {
            List<Defender> defenders = team.AllDefenders
                .Skip(CurrentDefenderPairIndex * 2)
                .Take(2)
                .ToList();

            List<Forward> forwards = team.AllForwards
                .Skip(CurrentForwardTripletIndex * 3)
                .Take(3)
                .ToList();

            Line newLine = new Line(defenders, forwards);
            team.SetCurrentLine(newLine);
        }

        private void PerformFullLineChange(Team team)
        {
            CurrentDefenderPairIndex = (CurrentDefenderPairIndex + 1) % 4;
            CurrentForwardTripletIndex = (CurrentForwardTripletIndex + 1) % 4;

            Defender[] defenders = team.AllDefenders
                .Skip(CurrentDefenderPairIndex * 2)
                .Take(2)
                .ToArray();

            Forward[] forwards = team.AllForwards
                .Skip(CurrentForwardTripletIndex * 3)
                .Take(3)
                .ToArray();

            team.SetLine(defenders, forwards);
        }

        private void PerformSinglePlayerChange(Team team)
        {
            Player mostTiredPlayer = FindMostTiredPlayer(team);

            if (mostTiredPlayer is Forward tiredForward)
            {
                ReplacePlayer(team, tiredForward);
            }
            else if (mostTiredPlayer is Defender tiredDefender)
            {
                ReplacePlayer(team, tiredDefender);
            }
        }

        private Player FindMostTiredPlayer(Team team)
        {
            Player mostTired = null;
            Single lowestEnergyRatio = Single.MaxValue;

            foreach (Defender defender in team.Defenders)
            {
                Single ratio = defender.Energy / defender.MaxEnergy;
                if (ratio < lowestEnergyRatio)
                {
                    lowestEnergyRatio = ratio;
                    mostTired = defender;
                }
            }

            foreach (Forward forward in team.Forwards)
            {
                Single ratio = forward.Energy / forward.MaxEnergy;
                if (ratio < lowestEnergyRatio)
                {
                    lowestEnergyRatio = ratio;
                    mostTired = forward;
                }
            }

            return mostTired;
        }

        private void ReplacePlayer(Team team, Player tiredPlayer)
        {
            Player freshestPlayer = null;
            Single highestEnergyRatio = Single.MinValue;

            foreach (Player benchPlayer in team.Bench)
            {
                if (tiredPlayer is Forward && !(benchPlayer is Forward)) continue;
                if (tiredPlayer is Defender && !(benchPlayer is Defender)) continue;

                Single ratio = benchPlayer.Energy / benchPlayer.MaxEnergy;
                if (ratio > highestEnergyRatio)
                {
                    highestEnergyRatio = ratio;
                    freshestPlayer = benchPlayer;
                }
            }

            if (freshestPlayer != null)
            {
                team.ReplacePlayerInLine(tiredPlayer, freshestPlayer);
            }
        }
    }
}
