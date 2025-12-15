using DetectiveGame;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace personalitytypes
{
    public enum DialogueState
    {
        AwaitingTopicSelection,
        AwaitingApproachSelection,
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
                    BeginTopicSelection();
                    break;

                case DialogueState.AwaitingApproachSelection:
                    BeginApproachSelection();
                    ResetDialogueIndex();
                    StartTopicConversation();
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

        private void BeginTopicSelection()
        {
            var availableTopics = npc.GetAvailableTopics();
            PromptTopicSelection(availableTopics);
            HandleTopicSelection(availableTopics);
        }


        private void PromptTopicSelection(List<string> availableTopics)
        {
            Console.WriteLine("Choose a topic, or choose 'quit' to go back.:");

            foreach (var topic in availableTopics)
            {
                Console.WriteLine($"- {topic}");
            }
        }

        private void HandleTopicSelection(List<string> availableTopics) // remember to call this 
        {
            string choice = Console.ReadLine().ToLower();

            while (!availableTopics.Contains(choice)) //immediately checks what the player inputs as their choice in the above line.
            {
                if (choice == "quit")
                {
                    State = DialogueState.ConversationEnded;
                    return;
                }
                Console.WriteLine("Invalid topic. Try again.");
                choice = Console.ReadLine().ToLower(); //allows user to have another chance to input topic selection
            }
            currentTopic = choice;
            State = DialogueState.AwaitingApproachSelection;
        }

        private void BeginApproachSelection()
        {
            PromptApproachSelection();
            var aproach = HandleApproachSelection();
            currentApproach = aproach;
        }

        private void PromptApproachSelection()
        {
            Console.WriteLine("Choose your approach: (G)entle or (D)irect?");
        }

        private string HandleApproachSelection()
        {
            while (true)
            {
                string approach = Console.ReadLine()?.ToLower();

                if (approach == "g" || approach == "d")
                    return approach;

                Console.WriteLine("Invalid choice, try 'g' or 'd'.");
            }
        }

        private void ResetDialogueIndex()
        {
            dialogueIndex = 0;
        }

        private void StartTopicConversation()
        {
            State = DialogueState.InTopic;
        }

        public int AdjustNpcMood()
        {
            int moodImpact = 0;

            if (currentApproach == "g") moodImpact += 2;
            else if (currentApproach == "d") moodImpact -= 3;

            npc.ChangeMood(moodImpact);
            return moodImpact;
        }

        public void AdjustDetectiveMood(int moodImpact)
        {
            if (npc is Suspect)
            {
                if (npc.Mood < 40 && moodImpact >= 0)
                {
                    detective.ChangeMood(+2);
                    Console.WriteLine($"{detective.Name} says: 'We're slowly getting somewhere.'");
                }
                else if (npc.Mood > 50 && moodImpact > 0)
                {
                    detective.ChangeMood(+2);
                    Console.WriteLine($"{detective.Name} says: 'I'm buttering this sucker up big time!'");
                }

                else if (npc.Mood < 50 && moodImpact < 0)
                {
                    detective.ChangeMood(-5);
                    Console.WriteLine($"{detective.Name} says: I'm not doing too well here");
                }

            }
            else if (npc is Witness)
            {
                if (moodImpact >= 0)
                {
                    detective.ChangeMood(+5);
                    Console.WriteLine($"{detective.Name} says: 'Good, I'm working well with this witness—they seem to be relaxing.'");
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
                int moodImpact = AdjustNpcMood(); //both giving a value to moodImpact and also running the method here (i dont like this, will look at in future. I want to run the method first, and then store a value after that, rather than two birds with one stone situation here!!!!
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

            currentTopic = null;
            currentApproach = null; // we are clearing up the fields so no bugs appear.
            State = DialogueState.AwaitingTopicSelection;
        }
    }
}
