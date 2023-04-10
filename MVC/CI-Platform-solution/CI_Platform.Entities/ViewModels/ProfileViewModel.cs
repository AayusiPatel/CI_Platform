using CI_Platform.Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entities.ViewModels
{
    public class ProfileViewModel
    {
        public long UserId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string Email { get; set; } = null!;

        public string? OldPassword { get; set; } 
        public string Password { get; set; } = null!;


        public string ConfirmPassword { get; set; } = null!;

        public long PhoneNumber { get; set; }

        public string? Avatar { get; set; }

        public IFormFile? Avatarfile { get; set; }

        public string? WhyIVolunteer { get; set; }

        public string? EmployeeId { get; set; }

        public string? Department { get; set; }

        public long? CityId { get; set; }

        public long? CountryId { get; set; }

        public string? ProfileText { get; set; }

        public string? LinkedInUrl { get; set; }

        public string? Title { get; set; }

        public List<UserSkill> userSkills { get; set; } = new List<UserSkill>();

        public List<Skill> skill { get; set; } = new List<Skill> { };

        public List<int>? skillsToAdd { get; set; }

      
    }

    public class ContactUsViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }
    }
    }
