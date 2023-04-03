using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using CI_Platform.Entities.Models;
using Microsoft.AspNetCore.Http;

namespace CI_Platform.Entities.ViewModels
{
    public class StoryModel
    {
       public  List<Story> stories { get; set; }
        public Story Story { get; set; }
        public List<StoryMedium> storiesMedium { get; set; }
        public List<User>? CoWorkers { get; set; }

        public int totalcount { get; set; }
        //public  List<StoryMedium> storyMedia { get; set; }
    }


    public class ShareStory
    {
       public  List<MissionApplication> missions { get; set; }
        public Story story { get; set; }
        public string? Stitle { get; set; }
        public string? Sdescription { get; set; }
        public DateTime? PublishedAt { get; set; }
        public long MissionId { get; set; }
        public List<IFormFile>? file { get; set; }

        public List<string> displayImages { get; set; } = new List<string> { };

    }
}
