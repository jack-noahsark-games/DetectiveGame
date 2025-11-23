//initial commit
using System;
using System.Collections.Generic;
namespace DetectiveGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Run();
        }
    }

    class Game
    {

        Detective sam = new Detective("Sam Somers", 35, 90, Person.PersonalityType.Calm);
        Suspect jake = new Suspect("Jake Miller", 28, 40, Person.PersonalityType.Hostile);
        Witness clara = new Witness("Clara White", 54, 55, Person.PersonalityType.Calm);
        Case activeCase = new Case("Case #001", "Murder at 14 Brook St.0");

        Dictionary<string, Person> people = new Dictionary<string, Person>();

        public enum GameState
        {
            MainMenu,
            Playing,
            Exiting
        }

        private GameState state = GameState.MainMenu;

        public void Run()
        {
            while (state != GameState.Exiting)
            {
                switch (state)
                {
                    case GameState.MainMenu:
                        MainMenu.Show();
                        string input = Console.ReadLine();
                        HandleInput(input);
                        break;
                    case GameState.Playing:
                        RunGameLoop();
                        break;
                }
            }
            Console.WriteLine("Thanks for playing!");

        }


        private void HandleInput(string input)
        {
            switch (input)
            {
                case "1":
                case "start":
                    state = GameState.Playing;
                    break;
                case "2":
                case "exit":
                    state = GameState.Exiting;
                    break;
                default:
                    Console.WriteLine("Invalid choice, please try again");
                    break;
            }
        }

        public Person ChoosePerson()
        {
            Console.WriteLine("Who do you want to question?");

            foreach (var entry in people)
            {
                Console.WriteLine($"- {entry.Key} ({entry.Value.GetType().Name})");
            }

            Console.WriteLine("\nType their name: ");
            string choice = Console.ReadLine().ToLower();

            if (people.ContainsKey(choice))
            {
                return people[choice];
            }

            Console.WriteLine("No person with that name.");
            return null;
        }

        private void RunGameLoop()
        {
            Console.WriteLine("Game has started! Type 'quit' to return to menu.");
            bool playing = true;
            people["clara"] = clara;
            people["jake"] = jake;

            while (playing)
            {
                string input = Console.ReadLine();

                if (input.ToLower() == "quit")
                {
                    state = GameState.MainMenu;
                    playing = false;
                }

                else
                {
                    Person target = ChoosePerson();

                    if (target != null)
                    {
                        sam.Question(target, activeCase);
                    }
                }
            }
        }

        static class MainMenu
        {
            public static void Show()
            {
                Console.Clear();
                Console.WriteLine("==== Untitled Detective Game ====");
                Console.WriteLine("1. Start Game");
                Console.WriteLine("2. Exit");
                Console.Write("\nChoose an option: ");
            }
        }
    }
}
