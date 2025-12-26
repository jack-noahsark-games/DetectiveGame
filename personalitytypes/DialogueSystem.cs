using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//initial push
namespace DetectiveGame
{
    public class DialogueSystem
    {
        private Person npc;
        private List<string> currentLines;
        private int dialogueIndex;
        private string currentTopic;




        public void StartTopic(Person npc, string topic) //store npc value and topic, store dialogue lines, reset dialogueIndex 
        {
            this.npc = npc;
            this.currentTopic = topic;

            currentLines = npc.GetDialogueForTopic(topic); //get the dialogue lines for the npc and topic

            dialogueIndex = 0;
        }

        public string GetNextLine() //check whether should continue, retrieve the line according to dialogueIndex
        {
            if (!HasNextLine())
                    return null;
            string line = currentLines[dialogueIndex];
            dialogueIndex++;
            return line;
        }

        public bool HasNextLine() //logic for continue/return false
        {
            if (currentLines == null)
                return false;

            return dialogueIndex < currentLines.Count; //if dialogue index less than count of total lines, return true. dynamic true/false return type here
        }

        public void EndTopic() //remove any leftover references to the variables, i.e. wipe the stored npc value, wipe currentLines
                               //when are we calling this method? at the end of a topic conversation, i.e. when there are no more lines to get.
        {
            npc = null;
            currentLines = null;
            currentTopic = null;
            dialogueIndex = 0;
        }
    }
}
