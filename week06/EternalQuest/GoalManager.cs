using System;
using System.Collections.Generic;
using System.IO;

namespace EternalQuestApp
{
    public class GoalManager
    {
        private readonly List<Goal> _goals = new();
        private int _score = 0;
        private int _level = 1;

        public void Run()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine($"Eternal Quest — Score: {_score}   Level: {_level}\n");
                Console.WriteLine("Menu Options:");
                Console.WriteLine("  1. Create New Goal");
                Console.WriteLine("  2. List Goals");
                Console.WriteLine("  3. Record Event");
                Console.WriteLine("  4. Save Goals");
                Console.WriteLine("  5. Load Goals");
                Console.WriteLine("  6. Quit");
                Console.Write("\nSelect a choice: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateGoal();
                        break;
                    case "2":
                        ListGoals();
                        break;
                    case "3":
                        RecordEvent();
                        break;
                    case "4":
                        SaveGoals();
                        break;
                    case "5":
                        LoadGoals();
                        break;
                    case "6":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Press Enter to continue...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private void CreateGoal()
        {
            Console.Clear();
            Console.WriteLine("Types of Goals:");
            Console.WriteLine("  1. Simple Goal");
            Console.WriteLine("  2. Eternal Goal");
            Console.WriteLine("  3. Checklist Goal");
            Console.Write("Select a choice: ");
            string? type = Console.ReadLine();

            Console.Write("Name: ");
            string name = Console.ReadLine() ?? "";
            Console.Write("Description: ");
            string description = Console.ReadLine() ?? "";
            int points = PromptInt("Points for each completion: ");

            switch (type)
            {
                case "1":
                    _goals.Add(new SimpleGoal(name, description, points));
                    break;
                case "2":
                    _goals.Add(new EternalGoal(name, description, points));
                    break;
                case "3":
                    int target = PromptInt("How many times to complete? ");
                    int bonus = PromptInt("Bonus points after final completion? ");
                    _goals.Add(new ChecklistGoal(name, description, points, target, bonus));
                    break;
                default:
                    Console.WriteLine("Invalid type. Press Enter to continue...");
                    Console.ReadLine();
                    break;
            }
        }

        private void ListGoals()
        {
            Console.Clear();
            Console.WriteLine("Your Goals:");
            if (_goals.Count == 0)
            {
                Console.WriteLine("  (none yet)");
            }
            for (int i = 0; i < _goals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_goals[i].GetStatusText()}");
            }
            Console.WriteLine("\nPress Enter to return to menu...");
            Console.ReadLine();
        }

        private void RecordEvent()
        {
            if (_goals.Count == 0)
            {
                Console.WriteLine("\nNo goals yet. Press Enter to continue...");
                Console.ReadLine();
                return;
            }

            Console.Write("\nWhich goal did you accomplish? Enter number: ");
            if (int.TryParse(Console.ReadLine(), out int idx))
            {
                if (idx < 1 || idx > _goals.Count)
                {
                    Console.WriteLine("Invalid number. Press Enter...");
                    Console.ReadLine();
                    return;
                }
                Goal g = _goals[idx - 1];
                int earned = g.RecordEvent();
                _score += earned;
                Console.WriteLine($"\nProgress recorded! You earned {earned} points.");
                // Level up every 1000 points
                int newLevel = (_score / 1000) + 1;
                if (newLevel > _level)
                {
                    _level = newLevel;
                    Console.WriteLine($"*** Congratulations! You leveled up to Level {_level}! ***");
                }
                Console.WriteLine($"New score: {_score}   Level: {_level}");
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }

        private void SaveGoals()
        {
            Console.Write("\nFilename to save to (e.g., goals.txt): ");
            string filename = Console.ReadLine() ?? "goals.txt";
            using var writer = new StreamWriter(filename);
            writer.WriteLine(_score);
            writer.WriteLine(_level);
            foreach (var g in _goals)
            {
                writer.WriteLine(g.GetStringRepresentation());
            }
            Console.WriteLine($"Saved {_goals.Count} goals and score to '{filename}'. Press Enter...");
            Console.ReadLine();
        }

        private void LoadGoals()
        {
            Console.Write("\nFilename to load from (e.g., goals.txt): ");
            string filename = Console.ReadLine() ?? "goals.txt";
            if (!File.Exists(filename))
            {
                Console.WriteLine("File not found. Press Enter...");
                Console.ReadLine();
                return;
            }
            var lines = File.ReadAllLines(filename);
            _goals.Clear();
            if (lines.Length == 0) return;

            if (int.TryParse(lines[0], out int sc)) _score = sc;
            if (lines.Length > 1 && int.TryParse(lines[1], out int lvl)) _level = lvl;

            for (int i = 2; i < lines.Length; i++)
            {
                var goal = ParseGoal(lines[i]);
                if (goal != null) _goals.Add(goal);
            }
            Console.WriteLine($"Loaded {_goals.Count} goals. Current score: {_score}. Level: {_level}. Press Enter...");
            Console.ReadLine();
        }

        private static Goal? ParseGoal(string line)
        {
            string[] parts = line.Split('|');
            try
            {
                switch (parts[0])
                {
                    case "Simple":
                        return new SimpleGoal(parts[1], parts[2], int.Parse(parts[3]), bool.Parse(parts[4]));
                    case "Eternal":
                        return EternalGoal.FromParts(parts);
                    case "Checklist":
                        return ChecklistGoal.FromParts(parts);
                    default:
                        return null;
                }
            }
            catch
            {
                return null;
            }
        }

        private static int PromptInt(string prompt)
        {
            Console.Write(prompt);
            while (true)
            {
                string? s = Console.ReadLine();
                if (int.TryParse(s, out int val) && val >= 0) return val;
                Console.Write("Enter a valid non-negative integer: ");
            }
        }
    }
}
