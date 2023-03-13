using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CI_Platform.Entities.Data;
using CI_Platform.Entities.Models;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Repository.Interface;
using CloudinaryDotNet.Actions;
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
        public List<City> GetCitys()
        {
            List<City> cities = _db.Cities.ToList();
            return cities;
        }
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
        public List<MissionSkill> GetSkills()
        {

            var skills = _db.MissionSkills.Include(m => m.Skill).ToList();
            List<MissionSkill> printskl = new List<MissionSkill>();
            foreach (var skill in skills)
            {
                bool chck = printskl.Any(x => x.SkillId == skill.SkillId);
                if(chck==false)
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

            PlatformModel missionCards = new PlatformModel();
            {

                missionCards.Mission = mission;
                missionCards.MissionThemes = missionThemes;
                missionCards.MissionSkill = missionSkills;
                missionCards.MissionMedium = missionMedia;
                missionCards.MissionRating = missionRatings;
                missionCards.Countries = countries;
                missionCards.Cities = cities;
            }
            return missionCards;
        }

       
        public List<Mission> GetMissionDetails()
        {
            List<Mission> missionDetails = _db.Missions.Include(m => m.City).Include(n => n.MissionTheme).ToList();
            //foreach (var item in missionDetails)
            //{ 
            //    Mission mission = new Mission();
            //    mission.MissionMedia=_db.MissionMedia.FirstOrDefault(x=>x.MissionId=missionDetails.MissionId)
            //}
            List<MissionMedium> missionMedia = _db.MissionMedia.ToList();
            var missions = (from z in missionDetails
                            join i in missionMedia on z.MissionId equals i.MissionId into tbl1
                            from i in tbl1.DefaultIfEmpty()
                            select z).ToList();

                return missions;
        }


        public List<Mission> Filter(List<int>? cityId, List<int>? countryId, List<int>? themeId, List<int>? skillId, string? search, int? sort)
        {
            List<Mission> cards = new List<Mission>();
            var missioncards = GetMissionDetails();
            var Missionskills = _db.MissionSkills.Include(m => m.Skill).ToList();
            List<int> temp = new List<int>();


            if (cityId.Count != 0 || countryId.Count != 0 || themeId.Count != 0 || skillId.Count != 0)
            {
               
            

                foreach (var n in countryId)
                {
                    foreach (var item in missioncards)
                    {
                        bool countrychek = cards.Any(x => x.MissionId == item.MissionId);
                        if (item.CountryId == n && countrychek == false)
                        {
                            cards.Add(item);
                        }
                    }

                }

                if (cityId.Count != 0 && countryId.Count != 0)
                {
                    cards.Clear();
                    foreach (var n in cityId)
                    {
                        foreach (var item in missioncards)
                        {
                            bool citychek = cards.Any(x => x.MissionId == item.MissionId);
                            if (item.CityId == n && citychek == false)
                            {
                                cards.Add(item);
                            }

                        }
                    }
                }


                foreach (var n in themeId)
                {
                    foreach (var item in missioncards)
                    {
                        bool themechek = cards.Any(x => x.MissionId == item.MissionId);
                        if (item.ThemeId == n && themechek == false)
                        {
                            cards.Add(item);
                        }
                    }
                }

                foreach (var n in skillId)
                {
                    foreach (var item in Missionskills)
                    {
                        bool skillchek = cards.Any(x => x.MissionId == item.MissionId);
                        if (item.SkillId == n && skillchek == false)
                        {

                            cards.Add(missioncards.FirstOrDefault(x => x.MissionId == item.MissionId));
                        }
                    }
                    //foreach (var item in Missionskills)
                    //{
                    //    if (item.SkillId == n)
                    //    {
                    //        temp.Add((int)item.MissionId);
                    //    }
                    //    foreach (var item2 in temp)
                    //    {
                    //        bool skillchek = missionDetails.Any(x => x.MissionId == item2);
                    //        if (skillchek == false)
                    //        {
                    //            cards.Add(missioncards.FirstOrDefault(x => x.MissionId == item2));
                    //        }
                    //    }

                //}
                }


                return cards;


            }

            else if (cityId.Count == 0 && countryId.Count == 0 && themeId.Count == 0 && skillId.Count == 0 && search == null)
            {
                //foreach (var item in missioncards)
                //{
                //    cards.Add(item);
                //}
                return missioncards;
            }

            if (search != null)
            {
                foreach (var n in missioncards)
                {
                    var title = n.Title.ToLower();
                    if (title.Contains(search.ToLower()))
                    {
                        cards.Add(n);
                    }
                }

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

            }
            return cards;

        }

    }
}

    


    

