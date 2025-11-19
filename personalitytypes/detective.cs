namespace DetectiveGame
{
    class Detective : Person
    {
        public Detective(string name, int age, int mood, PersonalityType personality) : base(name, age, mood, personality) { }

        public override void Speak()
        {
            Console.WriteLine($"{Name} says: 'I’m investigating a case.'");
        }

        public void Question(Person person, Case jealousMurder)
        {

            bool conversationFlow = true;

            int steps = 0;

            while (conversationFlow)
            {



                Console.WriteLine($"{Name} is about to question {person.Name}:");
                Console.WriteLine("Choose your approach: (G)entle or (D)irect?");
                string approach = Console.ReadLine().ToLower();

                if (string.IsNullOrWhiteSpace(approach) || (approach != "g" && approach != "d"))
                {
                    Console.WriteLine("Invalid choice, try 'g' or 'd'.");
                    continue;

                }


                person.RespondTo(this, approach, steps, out int moodImpact);
                AdjustAfterQuestion(person, moodImpact);
                jealousMurder.ProgressCase(person);
                steps += 1;

                if (steps >= person.PersonalityDialogue[person.Personality].Count)
                {

                    Console.WriteLine($"{person.Name} has nothing more to say.");
                    conversationFlow = false;


                }


                if (person.Mood < 30)
                {
                    Console.WriteLine($"{person.Name} has finished the conversation, you have caused their mood to drop too low.");
                    conversationFlow = false;
                    jealousMurder.ResetProgress();
                    
                }
            }
            Console.WriteLine();
        }

        public void AdjustAfterQuestion(Person person, int moodImpact)
        {
            if (person is Suspect)
            {
                if (person.Mood < 40 && moodImpact >= 0)
                {
                    ChangeMood(+2);
                    Console.WriteLine($"{Name} says: 'We're slowly getting somewhere.'");
                }
                else if (person.Mood > 50 && moodImpact > 0)
                {
                    ChangeMood(+2);
                    Console.WriteLine($"{Name} says: 'I'm buttering this sucker up big time!'");
                }

                else if (person.Mood < 50 && moodImpact < 0)
                {
                    ChangeMood(-5);
                    Console.WriteLine($"{Name} says: I'm not doing too well here");
                }

            }
            else if (person is Witness)
            {
                if (moodImpact >= 0)
                {
                    ChangeMood(+5);
                    Console.WriteLine($"{Name} says: 'Good, I'm working well with this witness—they seem to be relaxing.'");
                }
                else
                {
                    ChangeMood(-10);
                    Console.WriteLine($"{Name} says: 'Come on! They're a witness—I'm messing this up, they'll clam up soon.'");
                }
            }

            ShowMood();
        }
    }
}