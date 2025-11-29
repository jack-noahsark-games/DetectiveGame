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


        public void Question(Person person, Case activeCase)
        {
            var controller = new ConversationController(this, person, activeCase);

            while (controller.State != DialogueState.ConversationEnded) 
            {
                controller.Tick();
            
            }

        }
    }
}   