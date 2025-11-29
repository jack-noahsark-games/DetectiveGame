using System;


namespace DetectiveGame
{
     public class Evidence
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }

        public Evidence(string id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;

        }

        public override string ToString()
        {
            return $"{Name} - {Description}";
        }
    }
}
