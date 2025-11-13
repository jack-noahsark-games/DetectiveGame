namespace DetectiveGame
{


    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        private int _mood;

        public int Mood
        {
            get { return _mood; }
            set
            {
                // Clamp mood to a 1–100 range
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

        // Each person can respond when questioned
        public virtual void RespondTo(Person questioner, int moodImpact)
        {
            Console.WriteLine($"{Name} says: 'Hello {questioner.Name}.'");
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