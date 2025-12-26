using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectiveGame
{
    public class NPCTemplate
    {
        public List<string> possibleNames = new();

        public int minAge;

        public int maxAge;

        public int minMood;

        public int maxMood;

        public List<Person.PersonalityType> personalityTypes = new();
    }
}
