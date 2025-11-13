namespace DetectiveGame
{


    class Suspect : Person
    {
        public Suspect(string name, int age, int mood, PersonalityType personality) : base(name, age, mood, personality) { }

        public override void RespondTo(Person questioner, int moodImpact, string approach)
        {
            switch (Personality)
            {
                case PersonalityType.Hostile:
                    Console.WriteLine($"{Name} scowls 'You're talking a load of nonsense you collossal prat'.");
                    break;
                case PersonalityType.Nervous:
                    Console.WriteLine($"{Name} fidgets nervously. 'I... I don't know what you're talking about'");
                    break;
                case PersonalityType.Calm:
                    Console.WriteLine($"{Name} looks undisturbed. 'I'm sorry to tell you this, but you have the wrong person here");
                    break;
            }

            if (approach == "g") moodImpact += 2;
            else if (approach == "d") moodImpact -= 3;

            ChangeMood(moodImpact);
            ShowMood();
        }
            
    }
}

   