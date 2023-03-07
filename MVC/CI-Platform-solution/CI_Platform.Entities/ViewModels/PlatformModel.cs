using CI_Platform.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entities.ViewModels
{
    internal class PlatformModel
    {
    }
    public class CityViewModel
    {
        public List<Country> Countries { get; set; }
        public List<City> Cities { get; set; }
    }
}
