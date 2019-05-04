using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;

namespace ConsoleApp1
{

    public abstract class Entity
    {
        public int x { get; set; }

        public int Y { get; set; }

        public Entity(int x, int y)
        {
            this.x = x;
            this.Y = y;
        }

        public Entity(Entity entity)
        {
            this.x = entity.x;
            this.Y = entity.Y;

        }

        public abstract void print();
    }

    public class Player : Entity
    {
        private readonly int _z;

        public int z => _z;

        public Player(int x, int y, int z) : base(x, y)
        {
            this._z = z;
        }

        public Player(Entity entity, int z) : base(entity)
        {
            this._z = z;
        }

        public override void print()
        {
            Console.WriteLine("This is a Player.");

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Entity> entities = new List<Entity>();
            for (int i = 0; i < 10; i++)
            {
                entities.Add(new Player(i,10-i,0));
            }

            int index = 100;
            foreach (Entity entity in entities)
            {
                entity.x = index++;
            }

            foreach (var entity in entities)
            {
                Console.WriteLine(entity.x);
            }

            var p = (Player) entities[0];

            Console.WriteLine(entities[0] is Player);
            Console.WriteLine(entities[0] is Entity);

            Console.WriteLine(p is Player);
            Console.WriteLine(p is Entity);


            Console.ReadLine();

        }
    }
}
