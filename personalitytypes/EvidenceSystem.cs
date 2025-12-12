using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectiveGame
{
    public class EvidenceSystem
    {

        public Evidence TryUnlockEvidence(Person npc, string currentTopic, Case activeCase)
        {
            int mood = npc.Mood;
            Evidence ev = null;

            if (currentTopic == "alibi" && mood >= 60)
            {
                ev = activeCase.GetEvidenceById("bar_receipt"); //pass ID into GetEvidenceById
            }
            else if (currentTopic == "night" && mood >= 55)
            {
                ev = activeCase.GetEvidenceById("shouting_in_alley");  //pass ID into GetEvidenceById
            }
            else if (currentTopic == "victim" && mood >= 50)
            {
                ev = activeCase.GetEvidenceById("victim_unknown_man");  //pass ID into GetEvidenceById
            }

            if (ev != null)
            {
                activeCase.AddEvidence(ev);
            }

            return ev;
        }
    }
}
