using DetectiveGame;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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




        //just need a way now to integrate the advance dialogue stuff with the approach.
        private void AdvanceDialogue()
        {
            var dialogueLines = npc.GetDialogueForTopic(currentTopic);

            if (dialogueIndex < dialogueLines.Count)
            {
                npc.RespondTo(detective, currentApproach, dialogueIndex, currentTopic, out int moodImpact, out List<string> lines);
                detective.AdjustAfterQuestion(npc, moodImpact);
                detective.TryUnlockEvidence(npc, activeCase, currentTopic);
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
                Console.WriteLine(dialogueLines[dialogueIndex]);
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
