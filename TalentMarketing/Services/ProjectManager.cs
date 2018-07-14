using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalentMarketing.Models;

namespace TalentMarketing.Services
{
    public class ProjectManager
    {
        private DataContext _context;
        
        public ProjectManager()
        {
            _context = DataContext.GetContext();
        }

        public void CreateProject(User user, Project newProject)
        {
            user.OwnedProjects.Add(newProject);
            newProject.ProjectId = _context.Projects.Count + 1;
            _context.Projects.Add(newProject);
        }

        public void SetProjectFilled(int projectId)
        {
            Project project = GetExistingProject(projectId);

            if(project != null)
            {
                project.ProjectStatus = Project.Status.Filled;
            }
        }

        public void SetProjectOpen(int projectId)
        {
            Project project = GetExistingProject(projectId);

            if (project != null)
            {
                project.ProjectStatus = Project.Status.Recruiting;
            }
        }

        public void SetProjectCompleted(int projectId)
        {
            Project project = GetExistingProject(projectId);

            if (project != null)
            {
                project.ProjectStatus = Project.Status.Completed;
            }
        }

        public void SetProjectOwner(User user, int projectId)
        {
            Project project = GetExistingProject(projectId);

            if (project != null)
            {
                project.ProjectOwner = user;
            }
        }

        public void AddTeamMember(User user, int projectId)
        {
            Project project = GetExistingProject(projectId);

            if (project != null)
            {
                project.TeamMembers.Add(user);

                if(project.TeamMembers.Count >= project.TeamCapacity)
                {
                    SetProjectFilled(projectId);
                }
            }
        }

        public void RemoveTeamMember(User user, int projectId)
        {
            Project project = GetExistingProject(projectId);

            if (project != null)
            {
                project.TeamMembers.Remove(user);

                if (project.TeamMembers.Count < project.TeamCapacity)
                {
                    SetProjectOpen(projectId);
                }
            }
        }

        public void SetProjectTeamCapacity(int projectId, int newCapacity)
        {
            Project project = GetExistingProject(projectId);

            if (project != null)
            {
                if(newCapacity <= project.TeamCapacity)
                {
                    return;
                }
                else
                {
                    project.TeamCapacity = newCapacity;
                    SetProjectOpen(projectId);
                }
            }
        }

        public void SetProjectName(int projectId, string newProjectName)
        {
            Project project = GetExistingProject(projectId);

            if (project != null)
            {
                project.ProjectName = newProjectName;
            }
        }

        public Project GetExistingProject(int projectId)
        {
            try
            {
                return _context.Projects.First(p => p.ProjectId == projectId);
            }
            catch
            {
                return null;
            }
        }

        public void SetDesiredSkills(int projectId, List<Skill> skills)
        {
            Project project = GetExistingProject(projectId);

            if (project != null)
            {
                project.DesiredSkills = skills;
            }
        }

        public void SetSkillsToGain(int projectId, List<Skill> skills)
        {
            Project project = GetExistingProject(projectId);

            if (project != null)
            {
                project.SkillsToGain = skills;
            }
        }
    }
}
