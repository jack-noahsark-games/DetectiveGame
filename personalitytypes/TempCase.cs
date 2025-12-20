using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectiveGame
{
    public class TempCase
    {
        public string Titles { get; set; } //if we did not have =n new(); the List would not exist when we go to create it, this allows us to fill out the List.

        public string Descriptions { get; set; }

        public string Locations { get; set; }

        public string Suspects { get; set; }

        public string Witnesses { get; set; }

        public string EvidenceItems { get; set; }



    public TempCase(string titles, string descriptions, string locations, string suspects, string witnesses, string evidenceItems)
        {
            Titles = titles;
            Descriptions = descriptions;
            Locations = locations;
            Suspects = suspects;
            Witnesses = witnesses;
            EvidenceItems = evidenceItems;

        }

    }
}
