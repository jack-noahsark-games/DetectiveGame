//initial commit 
using personalitytypes;
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
        MoodSystem moodSystem;
        EvidenceSystem evidenceSystem;
        DialogueSystem dialogueSystem;
        CaseGenerator caseGenerator;
        NPCGenerator npcGenerator;

        public void SetUp() //enables me to add clara and jake to the list for activeCase (can't do this in the same section as where you create an object... stupid!)
        {
            sam = new Detective("Sam Somers", 35, 90, Person.PersonalityType.Calm);
            jake = new Suspect("Jake Miller", 28, 70, Person.PersonalityType.Hostile);
            clara = new Witness("Clara White", 54, 70, Person.PersonalityType.Calm);
            barReceipt = new Evidence("bar_receipt","Bar receipt", "A bar receipt from the Three Tuns pub");
            victimUnknownMan = new Evidence("victim_unknown_man", "Victim seen with unknown man: ", "Victim was seen with an unknown man walking home from work.");
            shoutingInAlley = new Evidence("shouting_in_alley", "Heard shouting in alley", "A man was shouting with the victim behind the Three Tuns Pub");
            activeCase = new Case("Case #001", "Murder at 14 Brook St.", new List<Person> { clara, jake }, new List<Evidence> { barReceipt, victimUnknownMan, shoutingInAlley});
            moodSystem = new MoodSystem();
            evidenceSystem = new EvidenceSystem();
            dialogueSystem = new DialogueSystem();
            caseGenerator = new CaseGenerator();
            npcGenerator = new NPCGenerator();
            
        }

        Dictionary<string, Person> people = new Dictionary<string, Person>(); //creates an empty dictionary and we add to it later on, this could do with changing.

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

        public Person ChoosePerson() // should be moved to another class later, also it is doing too much, handle input should be separate.
        {
            Console.WriteLine("Who do you want to question?");

            foreach (var entry in people)
            {
                Console.WriteLine($"- {entry.Key} ({entry.Value.GetType().Name})"); //gets the type of person (witness/suspect) dynamically
            }

            Console.WriteLine("\nType their name: ");
            string choice = Console.ReadLine().ToLower();

            if (people.ContainsKey(choice)) //OK so we're checking the hardcoded names in the dictionary here, but, do we want to hardcode a dictionary?
            {
                return people[choice];
            }

            Console.WriteLine("No person with that name.");
            return null;
        }

        private void RunGameLoop() //too many responsibilities, needs to be broken up later - Should not be creating things here!
        {
            SetUp();
            Console.WriteLine("Game has started! Type 'quit' to return to menu.");
            TempCase newCase = caseGenerator.GenerateCase();// temporary code to test case generation
            Person witness1 = npcGenerator.GenerateWitness(); //temporary code to test npc generation
            Console.WriteLine($"{witness1.GetType()}");//temporary debug line
            Console.WriteLine($"{witness1.Name} {witness1.Age} {witness1.Mood} {witness1.Personality}"); //temporary debug line
            Console.WriteLine($"===DEBUG TempCase object created, showing case Title : {newCase.Titles} ==="); //temporary debug line
            bool playing = true;
            people["clara"] = clara; //ok we are assigning a key to each person object here. We do this so when we make a choice, it points to the right object.
            people["jake"] = jake; //do we need to be doing this here? Can we do it somewhere else?

            while (playing)
            {
                string input = Console.ReadLine(); // this is causing the double enter/key press issue. review later.

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
                        sam.Question(target, activeCase, evidenceSystem, moodSystem, dialogueSystem);

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
