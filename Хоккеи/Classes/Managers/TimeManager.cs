using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Хоккеи.Classes.Managers
{
    public class TimeManager
    {
        public const Int32 TicksPerPeriod = 1200; 
        public const Int32 TotalPeriods = 3;
        public const Int32 BreakDurationTicks = 15; 

        public Int32 CurrentTick { get;  set; }
        public Int32 CurrentPeriod { get; private set; }
        public Boolean IsBreak { get; private set; }
        public Boolean IsMatchOver { get; private set; }
        public Int32 BreakTick { get; private set; }

        public event Action PeriodEnded;
        public event Action BreakStarted;
        public event Action BreakEnded;
        public event Action MatchEnded;

        public TimeManager()
        {
            CurrentTick = 0;
            CurrentPeriod = 1;
            IsBreak = false;
            IsMatchOver = false;
            BreakTick = 0;
        }

        public Int32 CurrentSecond
        {
            get { return CurrentTick % 60; }
        }

        public Int32 CurrentMinute
        {
            get { return (CurrentTick / 60) + 1; }
        }

        public Int32 TicksRemainingInPeriod
        {
            get { return TicksPerPeriod - CurrentTick; }
        }

        public Boolean IsLastMinuteOfPeriod()
        {
            return CurrentTick >= TicksPerPeriod - 60;
        }

        public void Tick()
        {
            if (IsMatchOver)
            {
                return;
            }

            if (IsBreak)
            {
                TickBreak();
                return;
            }

            TickPeriod();
        }

        private void TickPeriod()
        {
            CurrentTick++;

            if (CurrentTick >= TicksPerPeriod)
            {
                OnPeriodEnded();
            }
        }

        private void TickBreak()
        {
            BreakTick++;

            if (BreakTick >= BreakDurationTicks)
            {
                OnBreakEnded();
            }
        }

        private void OnPeriodEnded()
        {
            Console.WriteLine($"[DEBUG] OnPeriodEnded: Period={CurrentPeriod}, Tick={CurrentTick}");
            PeriodEnded?.Invoke();

            if (CurrentPeriod < TotalPeriods)
            {
                IsBreak = true;
                BreakTick = 0;
                Console.WriteLine($"[DEBUG] Starting break, BreakDurationTicks={BreakDurationTicks}");
                BreakStarted?.Invoke();
            }
            else
            {
                IsMatchOver = true;
                Console.WriteLine($"[DEBUG] Match over!");
                MatchEnded?.Invoke();
            }
        }

        private void OnBreakEnded()
        {
            Console.WriteLine($"[DEBUG] OnBreakEnded: BreakTick={BreakTick}, Period={CurrentPeriod}");
            IsBreak = false;
            CurrentTick = 0;
            BreakTick = 0;
            CurrentPeriod++;
            BreakEnded?.Invoke();
        }

        public String GetFormattedTime()
        {
            Int32 minutes = CurrentMinute - 1;
            Int32 seconds = CurrentSecond;
            return String.Format("P{0} {1:D2}:{2:D2}", CurrentPeriod, minutes, seconds);
        }

        public String GetFormattedTimeRemaining()
        {
            Int32 totalSecondsRemaining = TicksRemainingInPeriod;
            Int32 minutes = totalSecondsRemaining / 60;
            Int32 seconds = totalSecondsRemaining % 60;
            return String.Format("{0:D2}:{1:D2}", minutes, seconds);
        }
    }
}
