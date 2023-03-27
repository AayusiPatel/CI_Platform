using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CI_Platform.Entities.Models;

namespace CI_Platform.Entities.ViewModels
{
    public class StoryModel
    {
       public  List<Story> stories { get; set; }
        public Story Story { get; set; }
        public List<StoryMedium> storiesMedium { get; set; }
        public List<User>? CoWorkers { get; set; }
        //public  List<StoryMedium> storyMedia { get; set; }
    }
}
