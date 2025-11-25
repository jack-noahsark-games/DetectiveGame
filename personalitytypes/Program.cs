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

        Detective sam;
        Suspect jake;
        Witness clara;
        Case activeCase;
        Evidence barReceipt;
        Evidence victimUnknownMan;
        Evidence shoutingInAlley;

        public void SetUp() //enables me to add clara and jake to the list for activeCase (can't do this in the same section as where you create an object... stupid!)
        {
            sam = new Detective("Sam Somers", 35, 90, Person.PersonalityType.Calm);
            jake = new Suspect("Jake Miller", 28, 70, Person.PersonalityType.Hostile);
            clara = new Witness("Clara White", 54, 70, Person.PersonalityType.Calm);
            barReceipt = new Evidence("bar_receipt","Bar receipt", "A bar receipt from the Three Tuns pub");
            victimUnknownMan = new Evidence("victim_unknown_man", "Victim seen with unknown man: ", "Victim was seen with an unknown man walking home from work.");
            shoutingInAlley = new Evidence("shouting_in_alley", "Heard shouting in alley", "A man was shouting with the victim behind the Three Tuns Pub");
            activeCase = new Case("Case #001", "Murder at 14 Brook St.", new List<Person> { clara, jake }, new List<Evidence> { barReceipt, victimUnknownMan, shoutingInAlley});
        }

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
            SetUp();
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
                        activeCase.GetTotalEvidenceAmount();
                        Console.WriteLine(activeCase.GetTotalDialogueLines());
                        activeCase.GetRelevantNpcs();
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
