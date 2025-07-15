using System;

class Program
{
    static void Main()
    {
        
        Random randomGenerator = new Random();
        int magicNumber = randomGenerator.Next(1, 101); 

        int guess = -1;

        Console.WriteLine("Guess the magic number (1-100)");

        
        while (guess != magicNumber)
        {
            Console.Write("What is your guess? ");
            string input = Console.ReadLine();
            guess = int.Parse(input);

            if (guess < magicNumber)
            {
                Console.WriteLine("Higher");
            }
            else if (guess > magicNumber)
            {
                Console.WriteLine("Lower");
            }
            else
            {
                Console.WriteLine("You guessed it!");
            }
        }
    }
}
