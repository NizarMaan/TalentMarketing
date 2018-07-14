using System;
using System.Collections.Generic;

namespace TalentMarketing.Models
{
    public class Project
    {
        private int projectId;
        private string projectName;
        private int teamCapacity;
        private User projectOwner;
        private Status projectStatus;
        private List<Skill> desiredSkills;
        private List<Skill> skillsToGain; //list of skills a user would acquire upon project completion
        private readonly List<Application> applications; //cannot overwrite the variable, elements can still be added/removed
        private readonly List<User> teamMembers; //cannot overwrite the variable, elements can still be added/removed
        private int desiredSkillsLimit;
        private int skillsToGainLimit;

        public enum Status{
            Filled = 0,
            Recruiting = 1,
            Completed = 2
        }

        public Project(int projectId, string projectName, List<Skill> desiredSkills, List<Skill> skillsToGain, 
            User projectOwner, int teamCapacity)
        {
            this.projectName = projectName;
            this.desiredSkills = desiredSkills;
            this.skillsToGain = skillsToGain;
            this.projectOwner = projectOwner;
            this.teamCapacity = teamCapacity;

            projectId = -1;
            projectStatus = (Status) 1;
            teamMembers = new List<User>();
            applications = new List<Application>();
            desiredSkillsLimit = 30;
            skillsToGainLimit = 30;

            teamMembers.Add(projectOwner);
        }

        public int ProjectId
        {
            get { return projectId; }
            set
            {
                projectId = projectId < 0 ? value : projectId;
            }
        }

        public List<Application> Applications
        {
            get { return applications; }
        }

        public List<Skill> DesiredSkills
        {
            get { return desiredSkills; }
            set { desiredSkills = value; }
        }

        public List<Skill> SkillsToGain
        {
            get { return skillsToGain; }
            set { skillsToGain = value; }
        }

        public List<User> TeamMembers
        {
            get { return teamMembers; }
        }

        public Status ProjectStatus
        {
            get { return projectStatus; }
            set { projectStatus = value; }
        }

        public User ProjectOwner
        {
            get { return projectOwner; }
            set { projectOwner =  value; }
        }

        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }
        
        public int TeamCapacity
        {
            get { return teamCapacity; }
            set { teamCapacity = value; }
        }

        public int DesiresSkillsLimist
        {
            get { return desiredSkillsLimit; }
        }

        public int SkillsToGainLimit
        {
            get { return skillsToGainLimit; }
        }
    }
}