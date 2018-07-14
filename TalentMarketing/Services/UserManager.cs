using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalentMarketing.Models;

namespace TalentMarketing.Services
{
    public class UserManager
    {
        private DataContext _context;

        public UserManager()
        {
            _context = DataContext.GetContext();
        }

        //here we check if a user with the same email already exists before adding a new user
        public void AddUser(User user)
        {
            if(GetExistingUser(user.Email) == null)
            {
                user.UserId = _context.Users.Count + 1;
                _context.Users.Add(user);
            }
        }

        public void RemoveUser(int userId)
        {
            User userToRemove = GetExistingUser(userId);

            if (userToRemove != null)
            {
                _context.Users.Remove(userToRemove);
            }
        }

        public void AddSkill(int userId, Skill skill)
        {
            User user = GetExistingUser(userId);

            if (user != null)
            {
                if (GetExistingSkill(user.Skills, skill.Name) == null)
                {
                    user.Skills.Add(skill);
                }
            }
        }

        public void AddInterest(int userId, Skill interest)
        {
            User user = GetExistingUser(userId);

            if(user != null)
            {
                if (GetExistingSkill(user.Interests, interest.Name) != null)
                {
                    user.Interests.Add(interest);
                }
            }
        }

        public void RemoveSkill(int userId, string skillName)
        {
            User user = GetExistingUser(userId);

            if(user != null)
            {
                Skill skill = GetExistingSkill(user.Interests, skillName);

                if (skill != null)
                {
                    user.Skills.Remove(skill);
                }
            }
        }

        public void RemoveInterest(int userId, string interestName)
        {
            User user = GetExistingUser(userId);

            if(user != null)
            {
                Skill interest = GetExistingSkill(user.Interests, interestName);

                if (interest != null)
                {
                    user.Interests.Remove(interest);
                }
            }
        }

        public void EndorseSkill(int userId, string skillName)
        {
            User user = GetExistingUser(userId);

            if (user != null)
            {
                Skill skill = GetExistingSkill(user.Skills, skillName);

                //if skill being endorsed doesn't exist, create it
                if (skill == null)
                {
                    skill = new Skill(skillName);
                    user.Skills.Add(skill);
                }
                else
                {
                    skill.AddEndorsement();
                }
            }
        }

        public void DeductSkillEndorsement(int userId, string skillName)
        {
            User user = GetExistingUser(userId);

            if (user != null)
            {
                Skill skill = GetExistingSkill(user.Skills, skillName);

                if (skill != null)
                {
                    skill.DeductEndorsement();
                }

                if (skill.Endorsements <= 0)
                {
                    user.Skills.Remove(skill);
                }
            }
        }

        //helper method to check if a user exists via userId identifier
        public User GetExistingUser(int userId)
        {
            try
            {
                return _context.Users.First(u => u.UserId == userId);

            }
            catch
            {
                return null;
            }
        }

        //helper method to check if user exists via email identifier
        public User GetExistingUser(string email)
        {
            try
            {
                return _context.Users.First(u => u.Email == email);

            }
            catch
            {
                return null;
            }
        }

        //helper method to check if a skill/interest with identical name already exists in a Skill list
        public Skill GetExistingSkill(List<Skill> list, string name)
        {
            foreach (Skill s in list)
            {
                if (s.Name.ToLower().Equals(name.ToLower()))
                {
                    return s;
                }
            }

            return null;
        }

        public void SetFirstName(int userId, string newFirstName)
        {
            User user = GetExistingUser(userId);

            if(user != null)
            {
                user.FirstName = newFirstName;
            }
        }

        public void SetLastName(int userId, string newLastName)
        {
            User user = GetExistingUser(userId);

            if (user != null)
            {
                user.LastName = newLastName;
            }
        }

        public void SetEmail(int userId, string newEmail)
        {           
            if(GetExistingUser(newEmail) == null)
            {
                User user = GetExistingUser(userId);

                if (user != null)
                {
                    user.Email = newEmail;
                }
            }
        }

        public void SetCurrentTitle(int userId, string newTitle)
        {
            User user = GetExistingUser(userId);

            if (user != null)
            {
                user.CurrentTitle = newTitle;
            }
        }

        public void SetPhoneNumber(int userId, string newPhoneNumber)
        {
            User user = GetExistingUser(userId);

            if (user != null)
            {
                user.PhoneNumber = newPhoneNumber;
            }
        }
    }
}
