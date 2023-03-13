using CI_Platform.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entities.ViewModels
{
    public class PlatformModel
    {
        //public CI_Platform.Entities.Models.Mission? Mission { get; set; }

        public List<Mission> Mission { get; set; }
        public List<MissionTheme> MissionThemes { get; set; }

        public List<MissionSkill> MissionSkill { get; set; }

        public List<MissionMedium> MissionMedium { get; set; }

        public List<MissionRating>? MissionRating { get; set; }

        public List<Country>? Countries { get; set; }
        public List<City>? Cities { get; set; }
    }







}
