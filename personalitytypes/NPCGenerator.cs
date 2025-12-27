using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectiveGame
{
    public class NPCGenerator
    {

        private Random random = new Random(); //create a single Random instance for the class to use.

        private NPCTemplate template1 = new NPCTemplate //eventually have multiple templates to choose from tied to specific case types.
        {
            possibleNames =
            {
                "John Doe",
                "Jane Smith",
                "Bob Johnson",
                "Alice Williams",
                "Jack Somers",
                "Emily Davis",
                "Michael Brown"
            },

            minAge = 21,

            maxAge = 70,

            minMood = 10,

            maxMood = 90,

            personalityTypes =
            {
                Person.PersonalityType.Calm,
                Person.PersonalityType.Nervous,
                Person.PersonalityType.Cooperative,
                Person.PersonalityType.Hostile
            }
        };

        public string GenerateName()
        {
            return template1.possibleNames[random.Next(template1.possibleNames.Count)]; //returns a random name from the possibleNames list.
        }

        public int GenerateAge()
        {
            int randomAge = random.Next(template1.minAge, template1.maxAge +1);

            return randomAge;
        }

        public int GenerateMood()
        {
            int randomMood = random.Next(template1.minMood, template1.maxMood +1);
            return randomMood;
        }

        public Person.PersonalityType GeneratePersonalityType()
        {
            return template1.personalityTypes[random.Next(template1.personalityTypes.Count)]; //returns a random personality type from the personalityTypes list.
        }

        public List<Person> GenerateWitness() //generates a Witness NPC using the template data.
        {
            string name = GenerateName();
            int age = GenerateAge();
            int mood = GenerateMood();
            Person.PersonalityType personalityType = GeneratePersonalityType();

            return new Witness(name, age, mood, personalityType);
         }

        public List<Person> GenerateSuspect() //generates a Suspect NPC using the template data.
        {
            string name = GenerateName();
            int age = GenerateAge();
            int mood = GenerateMood();
            Person.PersonalityType personalityType = GeneratePersonalityType();
            return new Suspect(name, age, mood, personalityType);
        }

    }
}
