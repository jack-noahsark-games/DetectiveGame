using System.Runtime.CompilerServices;
using System.Collections.Generic;
namespace DetectiveGame
{
    class Witness : Person
    {
        public Witness(string name, int age, int mood, PersonalityType personality)
            : base(name, age, mood, personality)
        {
            PersonalityDialogue = new Dictionary<PersonalityType, Dictionary<string, List<string>>>(); // we create one dictionary here it is composed of,
                                                                                                       // a key which can be PersonalityType,
                                                                                                       // another Dictionary key, which will be topic and then finally a list inside there which will have lines of dialogue

            // CALM PERSONALITY //
            PersonalityDialogue[PersonalityType.Calm] = new Dictionary<string, List<string>>();

            PersonalityDialogue[PersonalityType.Calm]["night"] = new List<string>()
            {
                "I remember that night well, yes.",
                "There was shouting coming down that alleyway, behind the Three Tuns.",
                "Someone ran past me in a hurry.",
                "Tall mam, it was dark but I saw he had blonde hair with big sideburns."
            };

            PersonalityDialogue[PersonalityType.Calm]["alibi"] = new List<string>()
            {
                "I was at the Three Tuns at that time, yes. But I only came out the pub as I heard the shouting.",
                "People in the pub can vouch that I was in there all night, only left to go the toilet."
            };

            PersonalityDialogue[PersonalityType.Calm]["victim"] = new List<string>()
            {
                "The girl kept to themselves, she was quiet.",
                "I did see her from time to time walking with a fella after she finished work, never a good look at him though."
            };


            // HOSTILE PERSONALITY //
            PersonalityDialogue[PersonalityType.Hostile] = new Dictionary<string, List<string>>();

            PersonalityDialogue[PersonalityType.Hostile]["night"] = new List<string>()
            {
                "That night? What about it?",
                "I'm sorry but I don't keep track of every bloody sound do I?",
                "I heard two people arguing round the back alley, I was having a slash at the time, could hear through the window.",
                "No? Why would I have gone out? I'm not going to get involved when a fella and his bird are having a mardy."
            };

            PersonalityDialogue[PersonalityType.Hostile]["alibi"] = new List<string>()
            {
                "I told you, I was in the toilet when I heard them shouting.",
                "I don't even know the bird who got done in."
            };

            PersonalityDialogue[PersonalityType.Hostile]["victim"] = new List<string>()
            {
                "You're doing my head in mate, I told you already. I. Do. Not. Know. That. Woman."
            };
        }

        public override void RespondTo(Person questioner, string approach, int steps, out int moodImpact)
        {
            moodImpact = 0;
            Console.WriteLine($"DEBUG: {Name} personality is {Personality}");
            List<string> lines = PersonalityDialogue[Personality][approach];

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
