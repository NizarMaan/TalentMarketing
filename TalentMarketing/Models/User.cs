using System;
using System.Collections.Generic;

namespace TalentMarketing.Models
{
    public class User
    {
        private readonly List<Skill> skills;
        private readonly List<Skill> interests;
        private readonly List<Application> applications;
        private readonly List<Project> ownedProjects;
        private string firstName, lastName;
        private int userId;
        private string email;
        private string phoneNumber;
        private string currentTitle;
        private string password;

        public User(){
            skills = new List<Skill>();
            interests = new List<Skill>();
            applications = new List<Application>();
            userId = -1;
        }

        public User(string firstName, string lastName, string email, string phoneNumber, string currentTitle, string password){
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.phoneNumber = phoneNumber;
            this.currentTitle = currentTitle;
            this.password = password;

            userId = -1;
            skills = new List<Skill>();
            interests = new List<Skill>();
            applications = new List<Application>();
        }

        public List<Skill> Skills{
            get { return skills; }
        }

        public List<Skill> Interests{
            get {return interests; }
        }

        public List<Application> Applications
        {
            get { return applications; }
        }

        public List<Project> OwnedProjects
        {
            get { return ownedProjects; }
        }

        public int UserId
        {
            get { return userId; }
            set
            {
                if(userId < 0)
                {
                    userId = value;
                }
            }
        }

        public string FirstName{
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName{
            get { return lastName; }
            set { lastName = value; }
        }

        public string Email{
            get { return email; }
            set { email = value; }
        }

        public string PhoneNumber{
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        public string CurrentTitle{
            get { return currentTitle; }
            set { currentTitle = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
    }
}