using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalentMarketing.Models
{
    public class Review
    {
        public User reviewer;
        public User reviewee;
        public DateTime timeStamp; //time review was posted/writen
        public Project project; //project reviewed for
        public int rating;
        public int maxRating;
        public string reviewBody;

        public Review(User reviewer, User reviewee, Project project, int rating, string reviewBody)
        {
            this.reviewer = reviewer;
            this.reviewee = reviewee;
            this.project = project;
            this.rating = rating;
            this.reviewBody = reviewBody;
        }

        //Assume we have getters and setters below...
    }
}
