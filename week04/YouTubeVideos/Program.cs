using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        
        List<Video> videos = new List<Video>();

        Video video1 = new Video("Rock Crawling in Moab", "Tyler 4x4", 360);
        video1.AddComment(new Comment("Jake", "This trail looks gnarly!"));
        video1.AddComment(new Comment("Emma", "Loved the camera angles."));
        video1.AddComment(new Comment("Liam", "That Jeep is built right."));
        videos.Add(video1);

        Video video2 = new Video("RC Truck River Crossing", "RC Explorers", 285);
        video2.AddComment(new Comment("Sophia", "Awesome waterproof setup!"));
        video2.AddComment(new Comment("Noah", "How do you keep it so clean?"));
        video2.AddComment(new Comment("Olivia", "That was wild!"));
        videos.Add(video2);

        Video video3 = new Video("DIY Roof Rack Build", "Garage Fab", 540);
        video3.AddComment(new Comment("Mason", "Gonna try this next weekend."));
        video3.AddComment(new Comment("Ava", "Super helpful tutorial."));
        video3.AddComment(new Comment("Ethan", "Clean welds!"));
        videos.Add(video3);

        Video video4 = new Video("Night Ride in the Desert", "Midnight Motors", 420);
        video4.AddComment(new Comment("Isabella", "The lighting setup is killer."));
        video4.AddComment(new Comment("Lucas", "Gotta love those dunes."));
        video4.AddComment(new Comment("Mia", "That soundtrack was perfect."));
        videos.Add(video4);

        
        foreach (Video video in videos)
        {
            video.Display();
            Console.WriteLine();
        }
    }
}
