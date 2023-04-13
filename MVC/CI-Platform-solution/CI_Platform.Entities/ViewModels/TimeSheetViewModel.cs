using CI_Platform.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entities.ViewModels
{
    public class TimeSheetViewModel
    {
        public long TimesheetId { get; set; }

        public long UserId { get; set; }

        public long MissionId { get; set; }

        public TimeSpan? Time { get; set; }

        public int? Action { get; set; }

        public DateTime DateVolunteereed { get; set; }

        public string? Notes { get; set; }

        public string Status { get; set; } = null!;

        public List<Timesheet>   timecards { get; set; } = new List<Timesheet> { };
        public List<Timesheet> goalcards { get; set; } = new List<Timesheet> { };

        public List<Mission> timeMissions { get; set; } = new List<Mission> { };

        public List<Mission> goalMissions { get; set; } = new List<Mission> { };

        public int Hours { get; set; } 
        public int Minutes { get; set; } 
    }
}
