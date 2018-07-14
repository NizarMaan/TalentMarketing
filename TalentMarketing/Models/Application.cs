using System;

namespace TalentMarketing.Models
{
    public class Application
    {
        private DateTime applicationDate;
        private DateTime replyDate; //date on which application was denied/approved
        private Status appStatus;
        private User applicant;
        private Project projectApplyingTo;
        private int applicationId;

        public enum Status {
            Denied = -1,
            Pending = 0,
            Approved = 1,
            Withdrawn = 2,
            ProjectFilled = 3
        }

        public Application(User applicant, Project applyingTo)
        {
            applicationDate = DateTime.Now;
            appStatus = Status.Pending;
            applicationId = -1;

            this.applicant = applicant;
            this.projectApplyingTo = applyingTo;
        }

        public Application(User applicant, Project applyingTo, int applicationId)
        {
            applicationDate = DateTime.Now;
            appStatus = Status.Pending;

            this.applicationId = applicationId;
            this.applicant = applicant;
            this.projectApplyingTo = applyingTo;
        }

        public Status AppStatus{
            get { return appStatus; }
            set { appStatus = value; }
        }

        public DateTime ReplyDate
        {
            get { return replyDate; }
            set { replyDate = value; }
        }

        public DateTime ApplicationDate
        {
            get { return applicationDate; }
        }

        public User Applicant
        {
            get { return applicant; }
            set
            {
                applicant = applicant ?? value;
            }
        }

        public Project ProjectApplyingTo
        {
            get { return projectApplyingTo; }
            set
            {
                projectApplyingTo = projectApplyingTo ?? value;
            }
        }
        
        public int ApplicationId
        {
            get { return applicationId; }
            set
            {
                if (applicationId < 0)
                {
                    applicationId = value;
                }
            }
        }
    }
}