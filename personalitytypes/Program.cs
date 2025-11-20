//initial commit
using System;
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

        private void RunGameLoop()
        {
            Console.WriteLine("Game has started! Type 'quit' to return to menu.");
            bool playing = true;

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
                    sam.Question(clara, activeCase);
                    sam.Question(jake, activeCase);
                    Console.WriteLine($"You entered: {input}");
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
