using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CI_Platform.Entities.Data;
using CI_Platform.Entities.Models;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;


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
            List<City> cities = _db.Cities.ToList();
            return cities;
        }
        public List<City> GetCityData(int countryId)
        {

            List<City> city = _db.Cities.Where(i => i.CountryId == countryId).ToList();
            if (countryId == 0)
                city = _db.Cities.ToList();
            return city;

        }
        public List<MissionTheme> GetMissionTheme()
        {
            var theme = _db.MissionThemes.ToList();
            return theme;
        }
        public List<MissionSkill> GetSkills()
        {

            var skills = _db.MissionSkills.Include(m => m.Skill).ToList();
            List<MissionSkill> printskl = new List<MissionSkill>();
            foreach (var skill in skills)
            {
                bool chck = printskl.Any(x => x.SkillId == skill.SkillId);
                if (chck == false)
                { printskl.Add(skill); }
            }


            return printskl;

        }
        public PlatformModel GetMissions()
        {
            List<Mission> mission = _db.Missions.ToList();
            List<MissionMedium> missionMedia = _db.MissionMedia.Where(x => x.Default == 1).ToList();
            List<MissionSkill> missionSkills = _db.MissionSkills.ToList();
            List<MissionTheme> missionThemes = _db.MissionThemes.ToList();
            List<MissionRating> missionRatings = _db.MissionRatings.ToList();
            List<City> cities = _db.Cities.ToList();
            List<Country> countries = _db.Countries.ToList();
            List<GoalMission> goalMissions = _db.GoalMissions.ToList();
            int abc = (int)missionRatings.Average(x => x.Rating);

            int PageIndex = 1;
            int PageSize = 3;

            List<Mission> records = mission.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();

            int TotalRecords = (int)Math.Ceiling(mission.Count() / (double)PageSize);
           
         


            PlatformModel missionCards = new PlatformModel();
            {

                missionCards.Mission = records;
                missionCards.MissionThemes = missionThemes;
                missionCards.MissionSkill = missionSkills;
                missionCards.MissionMedium = missionMedia;
                missionCards.MissionRating = missionRatings;
                missionCards.Countries = countries;
                missionCards.Cities = cities;
                missionCards.GoalMission = goalMissions;
                missionCards.totalcount = TotalRecords;
                //missionCards.rating = abc;
            }



            return missionCards;
        }


        public List<Mission> GetMissionDetails()
        {
            List<Mission> missionDetails = _db.Missions.Include(m => m.City).Include(m => m.Theme).Include(m => m.MissionMedia).Include(m => m.GoalMissions).Include(m => m.MissionSkills).Include(m => m.MissionRatings).ToList();
            //foreach (var item in missionDetails)
            //{ 
            //    Mission mission = new Mission();
            //    mission.MissionMedia=_db.MissionMedia.FirstOrDefault(x=>x.MissionId=missionDetails.MissionId)
            //}
            return missionDetails;
        }


        public List<Mission> Filter(List<int>? cityId, List<int>? countryId, List<int>? themeId, List<int>? skillId, string? search, int? sort)
        {
            //List<Mission> cards = new List<Mission>();
            List<Mission> cards = GetMissionDetails();
            var Missionskills = _db.MissionSkills.Include(m => m.Skill).ToList();
            List<Mission> temp = new List<Mission>();

            if (countryId.Count > 0)
            {
                foreach (var n in countryId)
                {
                    cards = cards.Where(x => x.CountryId == n).ToList();
                }
            }
            if (cityId.Count > 0)
            {
                foreach (var n in cityId)
                {
                    cards = cards.Where(x => x.CityId == n).ToList();
                }
            }
            if (themeId.Count > 0)
            {
                cards = cards.Where(c => themeId.Contains((int)c.ThemeId)).ToList();
            }
            if (skillId.Count > 0)
            {

                //temp = cards.Where(x => skillId.Contains(x.MissionSkills.s).ToList();


                foreach (var n in skillId)
                {


                    temp.AddRange(cards.Where(x => x.MissionSkills.Any(x => x.SkillId == n)));

                    //cards.Add(missioncards.FirstOrDefault(x => x.MissionId == item.MissionId));

                }
                cards = temp;
            }
          

            if (search != null)
            {
                cards = cards.Where(a => a.Title.Contains(search) || a.OrganizationName.Contains(search)).ToList();
            }

           

            if (sort != null)
            {
                if (sort == 1)
                {

                    cards = cards.OrderByDescending(x => x.CreatedAt).ToList();

                }
                if (sort == 2)
                {

                    cards = cards.OrderBy(x => x.CreatedAt).ToList();

                }
                if (sort == 3)
                {

                    cards = cards.OrderBy(x => x.EndDate).ToList();

                }


                //if (sort == 4)
                //{
                //    if (cards.Count != 0)
                //    {

                //        cards = cards.OrderBy(x => x.FavoriteMissions).ToList();
                //    }

                //    else
                //    {
                //        missioncards = missioncards.OrderBy(x => x.FavoriteMissions).ToList();
                //    }
                //}

            }

           
           


            return cards;

        }








        public bool AddFav(int UserID, int MissionId)
        {
            FavoriteMission mission = new FavoriteMission();
            {
                mission.UserId = UserID;
                mission.MissionId = MissionId;
            }
            FavoriteMission favM = _db.FavoriteMissions.FirstOrDefault(x => x.UserId == mission.UserId && x.MissionId == MissionId);

            if (favM != null)
            {

                _db.FavoriteMissions.Remove(favM);
                _db.SaveChanges();
                return false;
            }
            else
            {
                _db.FavoriteMissions.Add(mission);
                _db.SaveChanges();
                return true;

            }



        }
    }
}

