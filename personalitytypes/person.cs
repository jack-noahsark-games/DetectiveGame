namespace DetectiveGame
{


    public abstract class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        private int _mood;

        public int Mood
        {
            get { return _mood; }
            set
            {
                if (value >= 1 && value <= 100)
                    _mood = value;
                else
                    _mood = 50;
            }
        }

        public enum PersonalityType
        {
            Calm,
            Nervous,
            Hostile,
            Cooperative
        }

        public PersonalityType Personality { get; set; }

        public Dictionary<PersonalityType, Dictionary<string, List<string>>> PersonalityDialogue { get; set; }

        public Person(string name, int age, int mood, PersonalityType personality)
        {
            Name = name;
            Age = age;
            Mood = mood;
            Personality = personality;
        }

        public virtual void Speak()
        {
            Console.WriteLine($"{Name} says hello.");
        }

        public virtual void RespondTo(Person questioner, string approach, out int moodImpact)
        {
            moodImpact = 0;
            Console.WriteLine($"{Name} says: 'Hello {questioner.Name}.'");
        }

        public List<string> GetAvailableTopics()
        {
            List<string> topics = new List<string>();

            foreach (var personalityEntry in PersonalityDialogue)
            {
                foreach (var topicEntry in personalityEntry.Value)
                {
                    string topicName = topicEntry.Key;

                    if (!topics.Contains(topicName))
                    {
                        topics.Add(topicName);
                    }
                }
            }

            return topics;
        }

        public List<string> GetDialogueForTopic(string topic)
        {
            return PersonalityDialogue[Personality][topic];
        }

        public void ChangeMood(int amount)
        {
            _mood += amount;
            if (_mood < 1) _mood = 1;
            if (_mood > 100) _mood = 100;
        }

        public void ShowMood()
        {
            Console.WriteLine($"{Name}'s mood is now {_mood}.");
        }
    }
}