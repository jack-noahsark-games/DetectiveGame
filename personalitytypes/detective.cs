using personalitytypes;
using System;
using System.ComponentModel.Design;
namespace DetectiveGame
{
    public class Detective : Person
    {
        public Detective(string name, int age, int mood, PersonalityType personality) : base(name, age, mood, personality) { }

        public override void Speak()
        {
            Console.WriteLine($"{Name} says: 'I’m investigating a case.'");
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
            var controller = new ConversationController(this, person, activeCase);

            while (controller.State != DialogueState.ConversationEnded) 
            {
                controller.Tick();
            
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