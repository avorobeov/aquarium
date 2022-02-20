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
            List<Fish> fishs = new List<Fish> { new Fish("Олив",1,10),
                                                new Fish("Аква", 1, 20),
                                                new Fish("Сега",4,30)};

            int maxCountFish = 10;

            Aquarium aquarium = new Aquarium(fishs, maxCountFish);

            string userInput;
            bool isExit = false;

            while (isExit == false)
            {
                ShowMenu();

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        aquarium.TryCreateFish();
                        break;

                    case "2":
                        aquarium.TryDeleteFish();
                        break;

                    case "3":
                        aquarium.StartGame();
                        break;

                    case "4":
                        aquarium.ShowFishs();
                        break;

                    case "5":
                        isExit = true;
                        break;
                }
            }
        }

        private static void ShowMenu()
        {
            ShowMessage("\nДля добавления рыбок нажмите 1\n" +
                     "\nДля удаления рыбок нажмите 2\n" +
                     "\nДля запуска игры нажмите 3\n" +
                     "\nПоказать всех рыбок 4\n"+
                     "\nДля выхода нажмите 5\n", ConsoleColor.Yellow);
        }

        private static void ShowMessage(string message, ConsoleColor color)
        {
            ConsoleColor preliminaryColor = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.WriteLine(message + "\n");

            Console.ForegroundColor = preliminaryColor;
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
   
        public void ReduceLife()
        {
            Age++;
        }
    }

    class Aquarium
    {
        private List<Fish> _fishs = new List<Fish>();

        private int _maxCountFish;
        private int _CountFish => _fishs.Count;
        private int _minAgeValue = 1;

        public Aquarium(List<Fish> fishs, int maxCountFish)
        {
            _fishs = fishs;
            _maxCountFish = maxCountFish;
        }

        public void StartGame()
        {
            while (_fishs.Count != 0)
            {
                for (int i = 0; i < _fishs.Count; i++)
                {
                    _fishs[i].ReduceLife();

                    if (_fishs[i].Age != _fishs[i].MaxAge)
                    {
                        ShowMessage($"Name: { _fishs[i].Name} Age:{ _fishs[i].Age} MaxAge:{ _fishs[i].MaxAge}", ConsoleColor.Blue);
                    }
                    else
                    {
                        TryDeleteFish(_fishs[i].Name);
                    }
                }
            }

            ShowMessage("Игра окончена все рыбки умерли", ConsoleColor.Red);
        }

        public void TryCreateFish()
        {
            string name;
            int age, maxAge;

            ShowMessage("Ведите имя рыбки", ConsoleColor.Yellow);

            name = Console.ReadLine();

            age = GetNumber("Ведите возраст рыбки");

            maxAge = GetNumber("Ведите максимальный возраст рыбки:");

            if (name != "" && age != _minAgeValue && maxAge > age && _CountFish <= _maxCountFish)
            {
                _fishs.Add(new Fish(name, age, maxAge));

                ShowMessage("Рыбка успешно добавлена в аквариум", ConsoleColor.Green);
            }
            else
            {
                ShowMessage("Ошибка ! \nДанные не прошли проверку", ConsoleColor.Red);
            }
        }

        private int GetNumber(string text)
        {
            string inputUser;
            int meaning = 0;
            bool isCorrect = false;

            while (isCorrect == false)
            {
                ShowMessage(text, ConsoleColor.Green);

                inputUser = Console.ReadLine();

                if (Int32.TryParse(inputUser, out meaning))
                {
                    return meaning;
                }
                else
                {
                    ShowMessage("Вы вели вместо числа строку", ConsoleColor.Red);
                }
            }

            return meaning;
        }
      
        public void TryDeleteFish(string name = "")
        {
            if (name == "")
            {
                ShowMessage("Ведите имя рыбки которую хотите достать", ConsoleColor.Yellow);

                name = Console.ReadLine();
            }

            Fish fish = _fishs.Find(fishName => fishName.Name.Contains(name));

            if (fish != null)
            {
                _fishs.Remove(fish);
            }
            else
            {
                ShowMessage("Такой рыбки нет в аквариуме", ConsoleColor.Red);
            }
        }

        public void ShowFishs()
        {
            if (_fishs.Count != 0)
            {
                ShowMessage("Список рыб", ConsoleColor.Yellow);
                for (int i = 0; i < _fishs.Count; i++)
                {
                    ShowMessage($"{_fishs[i].Name} её возраст {_fishs[i].Age}", ConsoleColor.Blue);
                }
            }
            else
            {
                ShowMessage("Сейчас аквариум пуст", ConsoleColor.Red);
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
