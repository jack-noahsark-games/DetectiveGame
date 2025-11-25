using System;
using System.ComponentModel.Design;
namespace DetectiveGame
{
    class Detective : Person
    {
        public Detective(string name, int age, int mood, PersonalityType personality) : base(name, age, mood, personality) { }

        public override void Speak()
        {
            Console.WriteLine($"{Name} says: 'I’m investigating a case.'");
        }

        public string GetApproach()
        {
            while (true)
            {
                Console.WriteLine("Choose your approach: (G)entle or (D)irect?");
                string approach = Console.ReadLine()?.ToLower();

                if (approach == "g" || approach == "d")
                    return approach;

                Console.WriteLine("Invalid choice, try 'g' or 'd'.");
            }
        }

        public string GetTopic()
        {
            while (true)
            {
                Console.WriteLine("Choose a topic: 'night', 'alibi' or 'victim'");
                string topic = Console.ReadLine()?.ToLower();

                if (topic == "night" || topic == "alibi" || topic =="victim")
                    return topic;

                Console.WriteLine("Invalid choice, try 'night', 'alibi' or 'victim'.");
            }
        }

        public (int moodImpact, List <string> lines) AskQuestionOnce
            (Person person, string topic, string approach, int dialogueIndex)
        {
            person.RespondTo(this, approach, dialogueIndex, topic, out int moodImpact, out List<string> lines);
            return (moodImpact, lines);
        }

        public bool ShouldEndConversation(Person person, int dialogueIndex, int totalLines)
        {
            if (dialogueIndex >= totalLines)
            {
                Console.WriteLine("Choose a new topic");
                return true;
            }

            if (person.Mood < 30)
            {
                Console.WriteLine($"{person.Name} has finished the conversation, you have caused their mood to drop too low.");
                return true;
            }

            return false;
        }
        public void EndConverstaion(Case activeCase) // maybe don't use this?
        {
            activeCase.ResetProgress();
        }

        public void AdjustAfterQuestion(Person person, int moodImpact)
        {
            if (person is Suspect)
            {
                if (person.Mood < 40 && moodImpact >= 0)
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
                if (moodImpact >= 0)
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
        }


        public void Question(Person person, Case activeCase)
        {
            int dialogueIndex = 0;

            while (true)
            {
                string approach = GetApproach();
                string topic = GetTopic();

                var result = AskQuestionOnce(person, topic, approach, dialogueIndex);
                int moodImpact = result.moodImpact;
                List<string> lines = result.lines;

                AdjustAfterQuestion(person, moodImpact);

                TryUnlockEvidence(person, activeCase, topic);
                activeCase.PrintEvidence();

                int totalLines = lines.Count;
                activeCase.ProgressCase(totalLines);

                dialogueIndex++;

                if (ShouldEndConversation(person, dialogueIndex, totalLines))
                {
                    break;
                }
                Console.WriteLine();

            }
        }

        public void TryUnlockEvidence(Person person, Case activeCase, string topic)
        {
            int mood = person.Mood;

            if (topic == "alibi" && mood >= 60)
            {
                var ev = activeCase.GetEvidenceById("bar_receipt"); //pass ID into GetEvidenceById
                if (ev != null) activeCase.AddEvidence(ev);//add evidence object to the FoundEvidence List

            }
            if (topic == "night" && mood >= 55)
            {
                var ev = activeCase.GetEvidenceById("shouting_in_alley");  //pass ID into GetEvidenceById
                if (ev != null) activeCase.AddEvidence(ev);//add evidence object to the FoundEvidence List
            }    
            if (topic == "victim" && mood >= 50)
            {
                var ev = activeCase.GetEvidenceById("victim_unknown_man");  //pass ID into GetEvidenceById
                if (ev != null) activeCase.AddEvidence(ev); //add evidence object to the FoundEvidence List
            }    
        }
    }
}   