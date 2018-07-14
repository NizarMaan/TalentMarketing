using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalentMarketing.Models
{
    //this is a class to encapsulate an applicants matched skills for a relevant project
    //an ApplicantMetaData object is passed to a project owner's view to render applicant's matching skills and other
    //relevant data
    public class ApplicationMetaData
    {
        private User user;
        private Project project;
        private Application application;
        private List<Skill> matchingSkills;

        public ApplicationMetaData(User user, Project project, Application application)
        {
            this.user = user;
            this.project = project;
            this.application = application;

            matchingSkills = new List<Skill>();
        }

        public User User
        {
            get { return user; }
            set { user = value; }
        }

        public Project Project
        {
            get { return project; }
            set { project = value; }
        }

        public Application Application
        {
            get { return application; }
            set { application = value; }
        }

        public List<Skill> MatchingSkills
        {
            get { return matchingSkills; }
        }

    }
}
