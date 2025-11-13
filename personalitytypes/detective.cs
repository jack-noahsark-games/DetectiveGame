namespace DetectiveGame
{
    class Detective : Person
    {
        public Detective(string name, int age, int mood, PersonalityType personality) : base(name, age, mood, personality) { }

        public override void Speak()
        {
            Console.WriteLine($"{Name} says: 'I’m investigating a case.'");
        }

        public void Question(Person person)
        {
            Console.WriteLine($"{Name} is about to question {person.Name}:");
            Console.WriteLine("Choose your approach: (G)entle or (D)irect?");
            string approach = Console.ReadLine().ToLower();

            int moodImpact = 0;
            if (person is Suspect)
                if (approach == "g")
                {
                    Console.WriteLine($"{Name} takes a gentle tone...");
                    moodImpact = +2; // gentle tone slightly improves their mood
                }
                else if (approach == "d")
                {
                    Console.WriteLine($"{Name} takes a direct tone...");
                    moodImpact = -10; // direct tone makes them tense
                }
                else
                {
                    Console.WriteLine($"{Name} hesitates and ends up mumbling an indecipherable jumble of words...");
                    moodImpact = 0;
                }
            if (person is Witness)
            {
                if (approach == "g")
                {
                    Console.WriteLine($"{Name} takes a gentle tone...");
                    moodImpact += 5;
                }

                else if (approach == "d")
                {
                    Console.WriteLine($"{Name} takes a direct tone...");
                    moodImpact = -5;
                }
            }

            // Apply the change to the person’s mood
            person.ChangeMood(moodImpact);

            // Let the person respond, passing the mood impact
            person.RespondTo(this, moodImpact);

            // Adjust Sam’s own reaction depending on how the talk went
            AdjustAfterQuestion(person, moodImpact);

            Console.WriteLine();
        }

        public void AdjustAfterQuestion(Person person, int moodImpact)
        {
            if (person is Suspect)
            {
                if (person.Mood < 40 && moodImpact > 0)
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
                if (moodImpact > 0)
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