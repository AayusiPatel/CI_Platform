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

            PlatformModel missionCards = new PlatformModel();
            {

                missionCards.Mission = mission;
                missionCards.MissionThemes = missionThemes;
                missionCards.MissionSkill = missionSkills;
                missionCards.MissionMedium = missionMedia;
                missionCards.MissionRating = missionRatings;
                missionCards.Countries = countries;
                missionCards.Cities = cities;
                missionCards.GoalMission = goalMissions;
                //missionCards.rating = abc;
            }
            return missionCards;
        }


        public List<Mission> GetMissionDetails()
        {
            List<Mission> missionDetails = _db.Missions.Include(m => m.City).Include(m => m.Theme).Include(m => m.MissionMedia).Include(m => m.GoalMissions).Include(m => m.MissionSkills).Include(m=>m.MissionRatings).ToList();
            //foreach (var item in missionDetails)
            //{ 
            //    Mission mission = new Mission();
            //    mission.MissionMedia=_db.MissionMedia.FirstOrDefault(x=>x.MissionId=missionDetails.MissionId)
            //}
            return missionDetails;
        }


        public List<Mission> Filter(List<int>? cityId, List<int>? countryId, List<int>? themeId, List<int>? skillId, string? search, int? sort)
        {
            List<Mission> cards = new List<Mission>();
            var missioncards = GetMissionDetails();
            var Missionskills = _db.MissionSkills.Include(m => m.Skill).ToList();
            List<int> temp = new List<int>();


            if (countryId.Count != 0 || themeId.Count != 0 || skillId.Count != 0)
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





            }

            if (countryId.Count == 0 && themeId.Count == 0 && skillId.Count == 0 && search == null)
            {
                foreach (var item in missioncards)
                {
                    cards.Add(item);
                }

            }

            if (search != null)
            {
                List<Mission> srch = new List<Mission>();


                if (cards.Count != 0)
                {
                    foreach (var n in cards)
                    {

                        var title = n.Title.ToLower();
                        if (title.Contains(search.ToLower()))
                        {
                            srch.Add(n);
                        }


                    }
                }

                if (cards.Count == 0)
                {
                    foreach (var n in missioncards)
                    {
                        var title = n.Title.ToLower();
                        if (title.Contains(search.ToLower()))
                        {
                            srch.Add(n);
                        }
                    }
                }
                cards = srch;

            }

            if (sort != null)
            {
                if (sort == 1)
                {
                    if (cards.Count != 0)
                    {
                        cards = cards.OrderByDescending(x => x.CreatedAt).ToList();
                    }

                    else
                    {
                        missioncards = missioncards.OrderByDescending(x => x.CreatedAt).ToList();
                    }
                }
                if (sort == 2)
                {
                    if (cards.Count != 0)
                    {
                        cards = cards.OrderBy(x => x.CreatedAt).ToList();
                    }

                    else
                    {
                        missioncards = missioncards.OrderBy(x => x.CreatedAt).ToList();
                    }
                }
                if (sort == 3)
                {
                    if (cards.Count != 0)
                    {
                        cards = cards.OrderBy(x => x.EndDate).ToList();
                    }

                    else
                    {
                        missioncards = missioncards.OrderBy(x => x.EndDate).ToList();
                    }
                }
                if (sort == 3)
                {
                    if (cards.Count != 0)
                    {
                        cards = cards.OrderBy(x => x.EndDate).ToList();
                    }

                    else
                    {
                        missioncards = missioncards.OrderBy(x => x.EndDate).ToList();
                    }
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






