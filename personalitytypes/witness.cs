namespace DetectiveGame
{


    class Witness : Person
    {
        public Witness(string name, int age, int mood, PersonalityType personality) : base(name, age, mood, personality) { }

        public override void RespondTo(Person questioner, string approach, out int moodImpact)
        {
            moodImpact = 0;
            Console.WriteLine($"DEBUG: {Name} personality is {Personality}");
            switch (Personality)
            {
                case PersonalityType.Hostile:
                    Console.WriteLine($"{Name} frowns 'I don't like talking to the police, I'm not a rat you know.'.");
                    break;
                case PersonalityType.Nervous:
                    Console.WriteLine($"{Name} looks around nervously. 'I don't know if I should talk to you, someone might be watching us...'");
                    break;
                case PersonalityType.Calm:
                    Console.WriteLine($"{Name} looks calm. 'I did see something suspicious that night actually, a man walking out the alley.'");
                    break;
                case PersonalityType.Cooperative:
                    Console.WriteLine($"{Name} looks eager to help. 'I saw a man that night, tall fellow, had a war wound I think across his face, shrapnel I suppose");
                    break;
            }

            if (approach == "g") moodImpact += 2;
            else if (approach == "d") moodImpact -= 3;

            ChangeMood(moodImpact);
            ShowMood();        
        }
    }
}