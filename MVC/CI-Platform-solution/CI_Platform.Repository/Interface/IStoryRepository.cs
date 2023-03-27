﻿using CI_Platform.Entities.Models;
using CI_Platform.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Interface
{
    public interface IStoryRepository
    {
        public StoryModel stories();
        public List<Story> StoryFilter(string? search);
        public StoryModel storyDetails(int sid, int uid);
        public void RecommandToCoWorker(int FromUserId, List<int> ToUserId, int sid);
    }
}
