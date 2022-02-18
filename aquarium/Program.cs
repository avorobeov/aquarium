using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aquarium
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    class Fish
    {
        public string Name { get; private set; }
        public int Age { get; private set; }
        public int MaxAge { get; private set; }

        public Fish(string name, int age, int maxAge)
        {
            Age = age;
            MaxAge = maxAge;
            Name = name;
        }
    }

    class Aquarium
    {
        private List<Fish> _fishs = new List<Fish>();
        private int _maxCountFish;

        public Aquarium(int maxCountFish)
        {
            _maxCountFish = maxCountFish;
        }

        public void TryCreateFish(string name, int age, int maxAge)
        {
            if (name != "" && age != 0 && maxAge > age)
            {
                _fishs.Add(new Fish(name, age, maxAge));
            }
            else
            {
                ShowMessage("Ошибка ! \nДанные не прошли проверку", ConsoleColor.Red);
            }
        }

        public void TryDeleteFish(string name)
        {
            for (int i = 0; i < _fishs.Count; i++)
            {
                if (_fishs[i].Name.Contains(name))
                {
                    _fishs.RemoveAt(i);
                }
            }
        }

        private void ShowMessage(string message, ConsoleColor color)
        {
            ConsoleColor preliminaryColor = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.WriteLine(message + "\n");

            Console.ForegroundColor = preliminaryColor;
        }
    }
}
