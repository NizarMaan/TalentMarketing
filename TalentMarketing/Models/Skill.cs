using System;

namespace TalentMarketing.Models
{
    public class Skill
    {
        private string name;
        private int endorsementCount;

        public Skill(string name){
            this.name = name;
            endorsementCount = 1;
        }

        public Skill(string name, int endorsementCount){
            this.name = name;
            this.endorsementCount = endorsementCount;
        }

        public string Name{
            get { return name; }
            set { name = value; }
        }

        public int Endorsements{
            get { return endorsementCount; }
            set { endorsementCount = value; }
        }

        public void AddEndorsement(){
            endorsementCount++;
        }

        public void DeductEndorsement(){
            endorsementCount--;
        }
    }
}
