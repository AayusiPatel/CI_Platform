using CI_Platform.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Interface
{
    public interface IProfileRepository
    {
        public ProfileViewModel getUser(int uid);

        public bool ContactUs(ContactUsViewModel obj);
        public bool updateUser(ProfileViewModel user, int uid);
        public TimeSheetViewModel GetActivities(int uid);
        public TimeSheetViewModel GetActivity(int obj, int uid);

        public bool AddActivity(TimeSheetViewModel obj, int uid);

        public bool UpdateActivity(TimeSheetViewModel obj);

        public bool DeleteActivity(int tid);

    }
}
