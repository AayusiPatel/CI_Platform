using CI_Platform.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entities.ViewModels
{
    public class VolunteerModel
    {
       public Mission mission { get; set; }

        public List<MissionMedium>? missionMedias { get; set;}

        public List<MissionDocument>? missionDocuments { get; set; }

        public List<Mission>? relatedMissions { get; set; }

        public List<MissionRating>? missionRatings { get; set; }


        public int rating { get; set; }

        public List<MissionSkill>? missionSkills { get; set; }

        public List<MissionApplication>? missionApplicaton { get; set; }
       public List<Comment> comments { get; set; }

        public string commentDescription { get; set; }


        public List<User>? CoWorkers { get; set; }

        public  List<FavoriteMission> favoriteMissions { get; set; }


        public List<MissionApplication>? volunteres { get; set; }
    }
}
