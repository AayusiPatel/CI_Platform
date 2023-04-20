using CI_Platform.Entities.Models;
using CI_Platform.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Interface
{
    public interface IVolunteerRepository
    {
        public List<MissionMedium> media(int mid);
        public List<MissionDocument> document(int mid);

        public VolunteerModel DisplayModel(int mid,int pageIndex);
        public int avgRating(int mid);

        //public bool addComment(VolunteerModel obj, int uid);
        public void RecommandToCoWorker(int FromUserId, List<int> ToUserId, int mid);
        public bool applyMission(int mid, int uid);

        public bool addComment(int mid, int uid, string comnt);
        public bool MissionRating(int userId, int mid, int rating);
        public List<User> recentVolunteers(int mid);
        //public List<MissionApplication> volunteers(int mid);
    }
}
