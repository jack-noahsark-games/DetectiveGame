using System.Runtime.CompilerServices;
namespace DetectiveGame
{
    class Witness : Person
    {
        public Witness(string name, int age, int mood, PersonalityType personality)
            : base(name, age, mood, personality)
        {
            PersonalityDialogue = new Dictionary<PersonalityType, List<string>>()
            {
                {
                    PersonalityType.Calm,
                    new List<string>()
                    {
                        "Oh, hello Detective.",
                        "Let me think… yes, I did see something.",
                        "A man rushed past me near the alley.",
                        "That's all I can remember."
                    }
                },
                {
                    PersonalityType.Hostile,
                    new List<string>()
                    {
                        "What do you want now?",
                        "I already told you I don’t know anything.",
                        "Stop asking stupid questions.",
                        "I'm done talking."
                    }
                }
            };
        }

        public override void RespondTo(Person questioner, string approach, int steps, out int moodImpact)
        {
            moodImpact = 0;
            Console.WriteLine($"DEBUG: {Name} personality is {Personality}");
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
