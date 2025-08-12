using System;

namespace EternalQuestApp
{
    public class ChecklistGoal : Goal
    {
        private int _targetCount;
        private int _currentCount;
        private int _bonusPoints;

        public ChecklistGoal(string name, string description, int points, int targetCount, int bonusPoints, int currentCount = 0)
            : base(name, description, points)
        {
            _targetCount = targetCount;
            _bonusPoints = bonusPoints;
            _currentCount = currentCount;
        }

        public override int RecordEvent()
        {
            if (IsComplete()) return 0;

            _currentCount++;
            int earned = _points;
            if (_currentCount >= _targetCount)
            {
                earned += _bonusPoints;
            }
            return earned;
        }

        public override bool IsComplete() => _currentCount >= _targetCount;

        public override string GetStatusText()
        {
            string box = IsComplete() ? "[X]" : "[ ]";
            return $"{box} {_name} ({_description}) — Completed {_currentCount}/{_targetCount}";
        }

        public override string GetStringRepresentation()
        {
            return $"Checklist|{_name}|{_description}|{_points}|{_targetCount}|{_bonusPoints}|{_currentCount}";
        }

        public static ChecklistGoal FromParts(string[] parts)
        {
            return new ChecklistGoal(parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[4]), int.Parse(parts[5]), int.Parse(parts[6]));
        }
    }
}
