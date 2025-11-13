namespace DetectiveGame
{


    class Witness : Person
    {
        public Witness(string name, int age, int mood, PersonalityType personality) : base(name, age, mood, personality) { }

        public override void RespondTo(Person questioner, int moodImpact)
        {
            if (moodImpact < 0)
            {
                Console.WriteLine($"{Name} says: 'Detective {questioner.Name}, that was a bit harsh!'");
            }
            else
            {
                Console.WriteLine($"{Name} says: 'Detective {questioner.Name}, I think I saw the suspect near the alley.'");
            }

            ShowMood();
        }
    }
}