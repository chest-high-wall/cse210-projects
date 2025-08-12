using System;

namespace EternalQuestApp
{
    public class EternalGoal : Goal
    {
        
        private int _streakCount;
        private DateTime _lastRecordedDate;

        public EternalGoal(string name, string description, int points, int streakCount = 0, DateTime? lastRecordedDate = null)
            : base(name, description, points)
        {
            _streakCount = streakCount;
            _lastRecordedDate = lastRecordedDate ?? DateTime.MinValue;
        }

        public override int RecordEvent()
        {
            DateTime today = DateTime.Today;
            if (_lastRecordedDate == DateTime.MinValue)
            {
                _streakCount = 1;
            }
            else
            {
                var days = (today - _lastRecordedDate.Date).Days;
                if (days == 0)
                {
                    _streakCount = Math.Max(1, _streakCount);
                }
                else if (days == 1)
                {
                    _streakCount++;
                }
                else
                {
                    _streakCount = 1; 
                }
            }
            _lastRecordedDate = today;

            // Bonus: +25 points per streak beyond the first
            int bonus = 25 * Math.Max(0, _streakCount - 1);
            return _points + bonus;
        }

        public override bool IsComplete() => false; 

        public override string GetStatusText()
        {
            string streak = _streakCount > 0 ? $"Streak: {_streakCount}d" : "Streak: 0d";
            return $"[∞] {_name} ({_description}) — {streak}";
        }

        public override string GetStringRepresentation()
        {
            
            string last = _lastRecordedDate == DateTime.MinValue ? "" : _lastRecordedDate.ToString("yyyy-MM-dd");
            return $"Eternal|{_name}|{_description}|{_points}|{_streakCount}|{last}";
        }

       
        public static EternalGoal FromParts(string[] parts)
        {
            
            int pts = int.Parse(parts[3]);
            int streak = parts.Length > 4 && parts[4] != "" ? int.Parse(parts[4]) : 0;
            DateTime? last = null;
            if (parts.Length > 5 && DateTime.TryParse(parts[5], out var dt)) last = dt;
            return new EternalGoal(parts[1], parts[2], pts, streak, last);
        }
    }
}
