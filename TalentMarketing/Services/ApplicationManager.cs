using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalentMarketing.Models;

namespace TalentMarketing.Services
{
    public class ApplicationManager
    {
        private DataContext _context;
        private ProjectManager _projectManager;

        public ApplicationManager()
        {
            _context = DataContext.GetContext();
            _projectManager = new ProjectManager();
        }

        public void AddNewApplication(User applicant, Project project)
        {
            if (applicant != null && project != null)
            {
                if(project.ProjectStatus == (Project.Status) 1) //if project is still open to recruitment
                {
                    //User: applicant, Project: project, int: applicationId
                    Application application = new Application(applicant, project, _context.Applications.Count + 1);

                    _context.Applications.Add(application);
                    applicant.Applications.Add(application);
                    project.Applications.Add(application);
                }
            }
        }

        public void WithdrawApplication(int applicationId)
        {
            Application application = GetExistingApplication(applicationId);

            if (application != null)
            {
                application.AppStatus = (Application.Status) 2;
            }
        }

        public void DenyApplication(int applicationId)
        {
            Application application = GetExistingApplication(applicationId);

            if (application != null)
            {
                application.AppStatus = (Application.Status) (-1);
                SetReplyTime(application);
            }
        }

        public void ApproveApplication(int applicationId)
        {
            Application application = GetExistingApplication(applicationId);

            Project.Status status = application.ProjectApplyingTo.ProjectStatus;

            if (application != null && status == Project.Status.Recruiting)
            {
                application.AppStatus = (Application.Status) 1;
                SetReplyTime(application);
            }
        }

        public void ClosePendingApplications(Project project)
        {
            foreach(Application app in project.Applications)
            {
                if(app.AppStatus == Application.Status.Pending)
                {
                    app.AppStatus = Application.Status.ProjectFilled;
                    SetReplyTime(app);
                }
            }
        }

        //we allow users to reapply in the event that they withdrew
        public void ReApply(int applicationId, Project project)
        {
            Application application = GetExistingApplication(applicationId);

            if(project.ProjectStatus == Project.Status.Recruiting)
            {
                if (application != null)
                {
                    if (application.AppStatus == Application.Status.Withdrawn)
                    {
                        application.AppStatus = Application.Status.Pending;
                    }
                }
            }
        }

        public Application GetExistingApplication(int applicationId)
        {
            try
            {
                return _context.Applications.First(a => a.ApplicationId == applicationId);
            }
            catch
            {
                return null;
            }
        }

        //set the date at which the application was replied to (i.e. date on which it was approved/denied)
        public void SetReplyTime(Application application)
        {
            application.ReplyDate = DateTime.Now;
        }
    }
}
