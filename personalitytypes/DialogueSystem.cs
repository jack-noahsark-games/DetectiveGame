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

        public bool IsTopicActive => currentLines != null;

        public void StartTopic(Person npc, string topic)
        {
            this.npc = npc;
            this.currentTopic = topic;

            currentLines = npc.GetDialogueForTopic(topic);
            dialogueIndex = 0;
        }

    }
}
