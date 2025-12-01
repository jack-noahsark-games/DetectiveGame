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


        public void StartTopic(Person npc, string topic) //load the values 
        {
            this.npc = npc;
            this.currentTopic = topic;

            currentLines = npc.GetDialogueForTopic(topic);

            dialogueIndex = 0;
        }

        public bool HasNextLine()
        {
            if (currentLines == null)
                return false;

            return dialogueIndex < currentLines.Count; //if dialogue index less than count of total lines, return true. dynamic true/false return type here
        }

        public string GetNextLine()
        {
            if (!HasNextLine())
                    return null;
            string line = currentLines[dialogueIndex];
            dialogueIndex++;
            return line;
        }

        public void EndTopic() //remove any leftover references to the variables, i.e. wipe the stored npc value, wipe currentLines
        {
            npc = null;
            currentLines = null;
            currentTopic = null;
            dialogueIndex = 0;
        }
    }
}
