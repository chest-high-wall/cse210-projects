using System;
using System.Collections.Generic;

namespace MindfulnessApp
{
    public class ListingActivity : Activity
    {
        private readonly List<string> _prompts = new()
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        public ListingActivity()
            : base("Listing Activity", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
        { }

        public void Run()
        {
            StartMessage();
            var rnd = new Random();
            Console.WriteLine($"\nPROMPT: {_prompts[rnd.Next(_prompts.Count)]}");
            Console.WriteLine("You may begin in:");
            ShowCountdown(5);

            var items = new List<string>();
            DateTime endTime = DateTime.Now.AddSeconds(_duration);
            while (DateTime.Now < endTime)
            {
                Console.Write("> ");
                string? input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    items.Add(input.Trim());
                }
            }

            Console.WriteLine($"\nYou listed {items.Count} items!");
            EndMessage();
        }
    }
}
