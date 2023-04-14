using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
       public  List<MissionApplication> missions { get; set; } = new List<MissionApplication>();

        public Story? story { get; set; }

        [Required(ErrorMessage ="Story Title is required.")]
        public string? Stitle { get; set; }

        [Required(ErrorMessage = "Story Description is required.")]
        public string? Sdescription { get; set; }

        [Required(ErrorMessage = "Published Date  is required.")]
        public DateTime? PublishedAt { get; set; }

        
        [Required(ErrorMessage = "Mission  is required.")]
        public long MissionId { get; set; }

        public List<IFormFile>? file { get; set; }
        public string? url { get; set; }
        public List<string> displayImages { get; set; } = new List<string> { };

    }
}
