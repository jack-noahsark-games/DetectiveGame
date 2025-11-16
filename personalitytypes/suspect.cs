namespace DetectiveGame
{


    class Suspect : Person
    {
        public Suspect(string name, int age, int mood, PersonalityType personality) 
            : base(name, age, mood, personality) 
        {
            PersonalityDialogue = new Dictionary<PersonalityType, List<string>>()
            {
                {
                    PersonalityType.Calm,
                    new List<string>()
                    {
                        "Hello detective, I was expecting you to come and visit.",
                        "Oh, the murder? It was a sad thing indeed. I'm happy to help where I can",
                        "Well, that night I was down the Three Tuns, so I'm afraid I can't be much help.",
                        "I don't like your insinuations detective, I think this conversation has come to a close."
                    }
                },
                {
                    PersonalityType.Hostile,
                    new List<string>()
                    {
                        "What it is detective, I haven't got time for more of your pointless questions",
                        "That murder had nothing to do with me, I was working that night.",
                        "I don't let people accuse me of stuff I didn't do, do you understand what I'm saying?",
                        "For both of our sakes, before this gets out of hand, you best turn around and go"
                    }
                }
            };
        }



        public override void RespondTo(Person questioner, string approach, int steps, out int moodImpact)
        {
            moodImpact = 0;

            List<string> lines = PersonalityDialogue[Personality];

            if (steps < lines.Count)
            {
                Console.WriteLine($"{Name}: {lines[steps]}");
            }
            else
            {
                Console.WriteLine($"{Name}: I have nothing more to say.");
            }

            if (approach == "g") moodImpact += 2;
            else if (approach == "d") moodImpact -= 3;

            ChangeMood(moodImpact);
            ShowMood();
        }
            
    }
}

   