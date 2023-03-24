using CI_Platform.Entities.Data;
using CI_Platform.Entities.Models;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Repository
{
    public class StoryRepository : IStoryRepository
    {
        public readonly CiPlatformContext _db;
        public StoryRepository(CiPlatformContext db)
        {
            _db = db;
        }

        public StoryModel stories()
        {
            List<Story> stories = _db.Stories.Include(m => m.StoryMedia).Include(m => m.Mission.Theme).Include(m => m.User).ToList();
            //List<StoryMedium> storyMedia = _db.StoryMedia.ToList();
            StoryModel storyModel = new StoryModel();
            {
                storyModel.stories = stories;

            }
            return storyModel;
        }



        public List<Story> StoryFilter(string? search)
        {
            List<Story> cards = new List<Story>();
            var missioncards = _db.Stories.Include(m => m.StoryMedia).Include(m => m.Mission).Include(m => m.Mission.Theme).Include(m => m.User).ToList();
            var Missionskills = _db.MissionSkills.Include(m => m.Skill).ToList();
            List<int> temp = new List<int>();



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
            return cards;


        }

    }

}




