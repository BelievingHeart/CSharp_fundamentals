using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQ
{
    class Player
    {
        public string Name { get; }
        public string TeamName { get; }
        public int X { get; }
        public int Y { get; }

        public Player(string name, string teamName, int x, int y)
        {
            this.Name = name;
            this.TeamName = teamName;
            this.X = x;
            this.Y = y;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var players = new List<Player>
            {
                new Player("Johe", "Honer", 100, 75), new Player("Micheal", "Sick", 65, 40),
                new Player("Kanna", "Honer", 40, 80), new Player("Light", "Guru", 60, 30),
            };

            // Anything that implements IEnumerable can be used as input to LINQ as well as foreach-loop, including array, List ...
            // Here `players` is the argument to the LINQ structure passed by reference
            var in_TeamHoner = from p in players
                let teamName = p.TeamName
                where teamName == "Honer"
                      orderby p.X descending 
                select new {p.Name, teamName = p.TeamName.ToLower()};

            // First time to invoke LINQ
            foreach (var honerMember in in_TeamHoner)
            {
                Console.WriteLine(honerMember);
            }

            // Change the state of the input to the LINQ, which is `players`
            players.Add(new Player("Nathan", "Honer", 1000, 1000));
            // And I query again
            Console.WriteLine("\nAfter inserting a new member to team Honer");
            foreach (var honerMember in in_TeamHoner)
            {
                Console.WriteLine(honerMember);
            }

            Console.ReadLine();
        }
    }
}
