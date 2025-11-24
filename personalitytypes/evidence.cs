using System;


namespace DetectiveGame
{
     class Evidence
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Evidence(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public override string ToString()
        {
            return $"{Name} - {Description}";
        }
    }
}
