using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectiveGame
{
    public class CaseTemplate
    {
        public List<string> Titles { get; set; } = new(); //if we did not have =n new(); the List would not exist when we go to create it, this allows us to fill out the List.

        public List<string> Descriptions { get; set; } = new();

        public List<string> Locations { get; set; } = new();

        public List<string> Suspects { get; set; } = new();

        public List<string> Witnesses { get; set; } = new();

        public List<string> EvidenceItems { get; set; } = new();
    }
}
