using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CI_Platform.Entities.Data;
using CI_Platform.Entities.Models;
using CI_Platform.Repository.Interface;


namespace CI_Platform.Repository.Repository
{
    public class PlatformRepository : IPlatformRepository
    {
        public readonly CiPlatformContext _db;


        public PlatformRepository(CiPlatformContext db)
        {
            _db = db;

        }
        public List<Country> GetCountry()
        {
            var country = _db.Countries.ToList();
            return country;
        }
        public List<City> GetCitys()
        {
            var city = _db.Cities.ToList();
            return city;
        }
        public List<MissionTheme> GetMissionTheme()
        {
            var theme = _db.MissionThemes.ToList();
            return theme;
        }

        public List<Mission> GetMissions()
        {

            var missions = _db.Missions.ToList();
            return missions;

        }

    }
}
