using System;
using System.Collections.Generic;
using TalentMarketing.Models;

namespace TalentMarketing.Services
{
    public class Analyzer
    {
        //singleton class for the Engine that computes recommendations
        public static Analyzer instance;

        public static Analyzer GetInstance(){
            if(instance == null){
                instance = new Analyzer();
            }

            return instance;
        }

        /// <summary>
        /// this matches skills in O(n * m * w * u) time. This can probably be improved.
        /// this computes a list of a project owners projects along with each of its applicant's matching skills
        /// and encapsulates all associated metadata in an ApplicationMetaData object
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<ApplicationMetaData> ComputeSkillMatches(User user)
        {
            ApplicationMetaData metaData;
            List<ApplicationMetaData> listOfApplicantMetaData = new List<ApplicationMetaData>();
            
            foreach(Project project in user.OwnedProjects)
            {
                foreach(Application application in project.Applications)
                {
                    metaData = new ApplicationMetaData(user, project, application);

                    foreach (Skill skill in application.Applicant.Skills)
                    {
                        foreach(Skill requiredSkill in project.DesiredSkills)
                        {
                            if (skill.Name.Equals(requiredSkill.Name))
                            {
                                metaData.MatchingSkills.Add(skill);
                            }
                        }
                    }

                    listOfApplicantMetaData.Add(metaData);
                }
            }
            return listOfApplicantMetaData;
        }
    }
}