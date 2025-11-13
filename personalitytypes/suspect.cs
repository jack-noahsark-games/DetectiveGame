namespace DetectiveGame
{


    class Suspect : Person
    {
        public Suspect(string name, int age, int mood, PersonalityType personality) : base(name, age, mood, personality) { }

        public override void RespondTo(Person questioner, int moodImpact)
        {
            if (moodImpact < 0)
            {
                Console.WriteLine($"{Name} says: 'Why don't you go stuff yourself, {questioner.Name}! I'm telling you nothing!'");
            }
            else
            {
                Console.WriteLine($"{Name} says: 'Why are you accusing me, {questioner.Name}?'");
            }

            ShowMood();
        }
    }
}

   