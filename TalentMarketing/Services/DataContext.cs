using System;
using System.Collections.Generic;
using System.Linq;
using TalentMarketing.Models;

namespace TalentMarketing.Services
{
    public class DataContext
    {
        //singleton class to store list of all projects, applications, and user objects
        public static DataContext context;
        public ICollection<User> Users;
        public ICollection<Project> Projects;
        public ICollection<Application> Applications;

        public static DataContext GetContext(){
            if(context == null){
                context = new DataContext();
            }

            return context;
        }

        //private constuctor, otherwise not singleton
        private DataContext(){
            Users = new List<User>();
            Projects = new List<Project>();
            Applications = new List<Application>();
        }
    }
}