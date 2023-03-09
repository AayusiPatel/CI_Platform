using CI_Platform.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Interface
{
    public interface IPlatformRepository
    {
        public List<Country> GetCountry();
        public List<City> GetCitys();

        public List<City> GetCityData(int countryId);
        public List<MissionTheme> GetMissionTheme();
        public void GetMissions();
        public List<Mission> GetMissionDetails();
        public List<MissionSkill> GetSkills();


        public List<Mission> Filter(List<int>? cityId, List<int>? countryId, List<int>? themeId, List<int>? skillId, string? search, int? sort);


    }
}
