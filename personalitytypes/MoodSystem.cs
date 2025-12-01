using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectiveGame
{
    public class MoodSystem
    {

        public int CalculateMoodImpact(Person npc, string topic, string approach)
        {
            int impact = 0;

            impact += ApplyPersonalityModifier(npc, topic);

            impact += ApplyApproachModifier(approach);

            impact += ApplyTopicModifier(topic);

            return impact;
        }

        public void ApplyMood(Person npc, int moodImpact)
        {
            npc.Mood += moodImpact;

            if (npc.Mood < 0) npc.Mood = 0;
            if (npc.Mood > 100) npc .Mood = 100;
        }

        private int ApplyPersonalityModifier(Person npc, string topic)
        {
            switch (npc.Personality)
            {
                case Person.PersonalityType.Calm:
                    return 0;

                case Person.PersonalityType.Nervous:
                    if (topic == "night") return -3;
                    return -1;

                case Person.PersonalityType.Hostile:
                    return -5;

                default:
                    return 0;
            }
        }

        private int ApplyApproachModifier(string approach)
        {
            switch (approach)
            {
                case "friendly": return +2;
                case "neutral": return 0;
                case "aggressive": return -3;
                default: return 0;
            }
        }

        private int ApplyTopicModifier(string topic)
        {
            if (topic == "victim") return +1;
            if (topic == "alibi") return 0;
            if (topic == "night") return -1;

            return 0;

        }
    }
}
