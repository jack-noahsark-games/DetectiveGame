using DetectiveGame;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace personalitytypes
{
    public enum DialogueState
    {
        AwaitingTopicSelection,
        InTopic,
        TopicComplete,
        ConversationEnded
    }
    public class ConversationController
    {
        private Detective detective;
        private Person npc;

        public DialogueState State { get; private set; }

        private string currentTopic;
        private string currentApproach;
        private int dialogueIndex;
        private Case activeCase;

        public ConversationController(Detective detective, Person npc, Case activeCase)
        {
            this.detective = detective;
            this.npc = npc;
            this.activeCase = activeCase;
            State = DialogueState.AwaitingTopicSelection;
        }

        public void Tick()
        {
            switch (State)
            {
                case DialogueState.AwaitingTopicSelection:
                    PromptTopicSelection();
                    break;

                case DialogueState.InTopic:
                    AdvanceDialogue();
                    break;

                case DialogueState.TopicComplete:
                    HandleTopicComplete();
                    break;

                case DialogueState.ConversationEnded:
                    Console.WriteLine("Conversation has ended.");
                    break;
            }
        }

        private void PromptTopicSelection()
        {
            Console.WriteLine("Choose a topic, or choose 'quit' to go back.:");

            var availableTopics = npc.GetAvailableTopics();

            foreach (var topic in availableTopics)
            {
                Console.WriteLine($"- {topic}");
            }

            string choice = Console.ReadLine().ToLower();

            while (!availableTopics.Contains(choice))
            {
                if (choice == "quit")
                {
                    State = DialogueState.ConversationEnded;
                    return;
                }
                Console.WriteLine("Invalid topic. Try again.");
                choice = Console.ReadLine().ToLower();
            }


            string approach = PromptApproachSelection();

            currentApproach = approach;
            currentTopic = choice;
            dialogueIndex = 0;
            State = DialogueState.InTopic;
        }

        private string PromptApproachSelection()
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

        public void AdjustDetectiveMood(int moodImpact)
        {
            if (npc is Suspect)
            {
                if (npc.Mood < 40 && moodImpact >= 0)
                {
                    detective.ChangeMood(+2);
                    Console.WriteLine($"{detective.Name}===NEW METHOD CHECK=== says: 'We're slowly getting somewhere.'");
                }
                else if (npc.Mood > 50 && moodImpact > 0)
                {
                    detective.ChangeMood(+2);
                    Console.WriteLine($"{detective.Name}===NEW METHOD CHECK===  says: 'I'm buttering this sucker up big time!'");
                }

                else if (npc.Mood < 50 && moodImpact < 0)
                {
                    detective.ChangeMood(-5);
                    Console.WriteLine($"{detective.Name}===NEW METHOD CHECK=== says: I'm not doing too well here");
                }

            }
            else if (npc is Witness)
            {
                if (moodImpact >= 0)
                {
                    detective.ChangeMood(+5);
                    Console.WriteLine($"{detective.Name}===NEW METHOD CHECK=== says: 'Good, I'm working well with this witness—they seem to be relaxing.'");
                }
                else
                {
                    detective.ChangeMood(-10);
                    Console.WriteLine($"{detective.Name}===NEW METHOD CHECK=== says: 'Come on! They're a witness—I'm messing this up, they'll clam up soon.'");
                }
            }
        }

        public void TryUnlockEvidence()
        {
            int mood = npc.Mood;

            if (currentTopic == "alibi" && mood >= 60)
            {
                var ev = activeCase.GetEvidenceById("bar_receipt"); //pass ID into GetEvidenceById
                if (ev != null) activeCase.AddEvidence(ev);//add evidence object to the FoundEvidence List

            }
            if (currentTopic == "night" && mood >= 55)
            {
                var ev = activeCase.GetEvidenceById("shouting_in_alley");  //pass ID into GetEvidenceById
                if (ev != null) activeCase.AddEvidence(ev);//add evidence object to the FoundEvidence List
            }
            if (currentTopic == "victim" && mood >= 50)
            {
                var ev = activeCase.GetEvidenceById("victim_unknown_man");  //pass ID into GetEvidenceById
                if (ev != null) activeCase.AddEvidence(ev); //add evidence object to the FoundEvidence List
            }
        }



        //just need a way now to integrate the advance dialogue stuff with the approach.
        private void AdvanceDialogue()
        {
            var dialogueLines = npc.GetDialogueForTopic(currentTopic);

            if (dialogueIndex < dialogueLines.Count)
            {

                Console.WriteLine($"{npc.Name}: {dialogueLines[dialogueIndex]}");
                npc.RespondTo(detective, currentApproach, out int moodImpact);
                AdjustDetectiveMood(moodImpact);
                TryUnlockEvidence();
                activeCase.PrintEvidence();
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
                dialogueIndex++;
            }
            else
            {
                State = DialogueState.TopicComplete;
            }    
        }

        private void HandleTopicComplete()
        {
            Console.WriteLine($"You have finished this topic.");
            npc.ShowMood();
            State = DialogueState.AwaitingTopicSelection;
        }
    }
}
