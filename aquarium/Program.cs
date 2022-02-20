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
            List<Fish> fishes = new List<Fish> { new Fish("Олив",1,10),
                                                new Fish("Аква", 1, 20),
                                                new Fish("Сега",4,30)};

            int NumberLives = 10;

            Aquarium aquarium = new Aquarium(fishes, NumberLives);

            string userInput;
            bool isExit = false;

            while (isExit == false)
            {
                aquarium.TimeSkip();

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
                        aquarium.ShowFishs();
                        break;

                    case "4":
                        isExit = true;
                        break;
                }

                if (aquarium.CountFishes == 0)
                {
                    ShowMessage("Игра окончена все рыбки умерли", ConsoleColor.Red);
                }
            }
        }

        private static void ShowMenu()
        {
            ShowMessage("\nДля добавления рыбок нажмите 1\n" +
                     "\nДля удаления рыбок нажмите 2\n" +
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
        public int NumberLives { get; private set; }

        private int _minimumNumberLives = 0;

        public Fish(string name, int age, int numberLives)
        {
            Age = age;
            NumberLives = numberLives;
            Name = name;
        }
   
        public void ReduceLife()
        {
            if (NumberLives > _minimumNumberLives)
            {
                Age++;
                NumberLives--;
            }
        }

        public bool GetLives()
        {
            if (NumberLives <= _minimumNumberLives)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    class Aquarium
    {
        private List<Fish> _fishes = new List<Fish>();
        public int CountFishes => _fishes.Count;

        private int _maxCountFishes;
        private int _minAgeValue = 1;

        public Aquarium(List<Fish> fishs, int maxCountFish)
        {
            _fishes = fishs;
            _maxCountFishes = maxCountFish;
        }

        public void TimeSkip()
        {
            for (int i = 0; i < _fishes.Count; i++)
            {
                _fishes[i].ReduceLife();

                ShowMessage($"Index:{i} Name: { _fishes[i].Name} Age:{ _fishes[i].Age} NumberLives:{ _fishes[i].NumberLives} IsLives:{ _fishes[i].GetLives()}", ConsoleColor.Blue);
            }
        
        }

        public void TryCreateFish()
        {
            string name;
            int age, numberLives;

            ShowMessage("Ведите имя рыбки", ConsoleColor.Yellow);

            name = Console.ReadLine();

            age = GetNumber("Ведите возраст рыбки");

            numberLives = GetNumber("Ведите количество жизней рыбки:");

            if (name != "" && age != _minAgeValue && numberLives > age && CountFishes <= _maxCountFishes)
            {
                _fishes.Add(new Fish(name, age, numberLives));

                ShowMessage("Рыбка успешно добавлена в аквариум", ConsoleColor.Green);
            }
            else
            {
                ShowMessage("Ошибка ! \nДанные не прошли проверку", ConsoleColor.Red);
            }
        }

        public void TryDeleteFish()
        {
            int index = GetNumber("Ведите номер рыбки которую хотите достать");

            for (int i = 0; i < _fishes.Count -1; i++)
            {
                if (index == i)
                {
                    _fishes.RemoveAt(i);
                }
            }
        }

        public void ShowFishs()
        {
            if (_fishes.Count != 0)
            {
                ShowMessage("Список рыб", ConsoleColor.Yellow);
                for (int i = 0; i < _fishes.Count; i++)
                {
                    ShowMessage($"{_fishes[i].Name} её возраст {_fishes[i].Age}", ConsoleColor.Blue);
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

    }
}
