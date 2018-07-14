using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TalentMarketing.Models;
using TalentMarketing.Services;

namespace TalentMarketing.Controllers
{
    //Whatever front-end routing logic is required, a lot of these views don't exist, this is a just a mock-up
    //Not all would-be routing/front-end functions are exemplified some are provided to show the gist of the design
    public class HomeController : Controller
    {
        private DataContext _context;
        private UserManager _userManager;
        private ProjectManager _projectManager;
        private ApplicationManager _applicationManager;
        private Analyzer _analyzer;
        
        //in ASP.NET, the below constructor and any arguments are passed by dependency injection behind the scenes
        public HomeController(DataContext context, UserManager userManager, ProjectManager projectManager, 
            ApplicationManager applicationManager, Analyzer analyzer)
        {
            _context = context;
            _userManager = userManager;
            _applicationManager = applicationManager;
            _analyzer = analyzer;
        }

        [HttpGet]
        public IActionResult CreateUser() {
            User model = new User();
            return View(model);         //return view that will hold and submit new user data
        }

        //takes as argument the user model that was submitted from the CreateUser view above
        [HttpPost]
        public IActionResult CreateUser(User user){
            _userManager.AddUser(user);
            return RedirectToAction("Confirmation", "Home"); //redirect to a non-existent confirmation page
        }

        //API POST route to be used as an AJAX call every time a skill is added via the view/UI
        [HttpPost]
        public void AddSkill(int userId, string skillName)
        {
            _userManager.AddSkill(userId, new Skill(skillName));
        }

        //API POST route to be used as an AJAX call every time an interest is added via the view/UI
        [HttpPost]
        public void AddInterest(int userId, string interestName)
        {
            _userManager.AddInterest(userId, new Skill(interestName));
        }

        //POST to be used when application form is submitted
        [HttpPost]
        public void ApplyToProject(int userId, int projectId)
        {
            User user = _userManager.GetExistingUser(userId);
            Project project = _projectManager.GetExistingProject(projectId);

            _applicationManager.AddNewApplication(user, project);
        }

        //POST to be used when application form is withdrawn
        [HttpPost]
        public void WithdrawApplication(int applicationId)
        {
            _applicationManager.WithdrawApplication(applicationId);
        }

        [HttpPost]
        public void CloseProjectRecruitment(int projectId)
        {
            Project project = _projectManager.GetExistingProject(projectId);

            if(project != null)
            {
                _applicationManager.ClosePendingApplications(project);
            }
        }

        //a mock route that serves up a view called "OwnedProjects"
        //here it is assumed that we have the userId from some sort of cookie or session tracker
        [HttpGet]
        public IActionResult OwnedProjects()
        {
            List<ApplicationMetaData> metadata = new List<ApplicationMetaData>();

            int userId = ;//retrieve current user session and their id

            User user = _userManager.GetExistingUser(userId);

            //pass the metadata to view...front-end logic will display the computed skill matches
            //for each applicant...assuming here that front-end logic will also handle if analyzer returns an empty list
            //this would mean that this user has no owned projects
            if (user != null)
            {
                metadata = _analyzer.ComputeSkillMatches(user);
            }

            return View(metadata);
        }
        
        //etc...

        //default routes as part of .Net project
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
