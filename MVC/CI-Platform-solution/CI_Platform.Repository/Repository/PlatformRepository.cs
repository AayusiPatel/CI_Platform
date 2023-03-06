using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CI_Platform.Entities.Data;
using CI_Platform.Entities.Models;
using CI_Platform.Repository.Interface;
using Microsoft.EntityFrameworkCore;

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
        //public List<City> GetCitys()
        //{
        //    List<City> cities = _db.Cities.ToList();
        //    return cities;
        //}
        public List<City> GetCityData(int countryId)
        {

            List<City> city = _db.Cities.Where(i => i.CountryId == countryId).ToList();
            return city;

        }
        public List<MissionTheme> GetMissionTheme()
        {
            var theme = _db.MissionThemes.ToList();
            return theme;
        }
        public List<Skill> GetSkills()
        {

            var skills = _db.Skills.ToList();
            return skills;

        }

        public List<Mission> GetMissions()
        {

            var missions = _db.Missions.ToList();
            return missions;

        }
        public List<Mission> GetMissionDetails()
        {
            List<Mission> missionDetails = _db.Missions.Include(m => m.City).Include(m => m.Theme).Include(m => m.MissionMedia).ToList();
            return missionDetails;
        }

    }
}
